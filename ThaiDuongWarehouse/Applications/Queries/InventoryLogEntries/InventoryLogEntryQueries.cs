namespace ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;

public class InventoryLogEntryQueries : IInventoryLogEntryQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;

    private IQueryable<InventoryLogEntry> _logs => _context.InventoryLogEntries
                                                           .Include(log => log.Item)
                                                           .AsNoTracking();
    public InventoryLogEntryQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InventoryLogEntryViewModel>> GetEntriesByItem(string itemId, TimeRangeQuery query)
    {
        var logEntries = await _logs
            .Where(log => log.Item.ItemId == itemId)
            .Where(log =>
            log.TrackingTime.CompareTo(query.StartTime) >= 0 &&
            log.TrackingTime.CompareTo(query.EndTime) <= 0)
            .Skip((query.Page - 1) * query.ItemsPerPage)
            .Take(query.ItemsPerPage)
            .ToListAsync();

        return _mapper.Map<IEnumerable<InventoryLogEntry>, IEnumerable<InventoryLogEntryViewModel>>(logEntries);
    }

    public async Task<IEnumerable<InventoryLogEntryViewModel>> GetEntries(TimeRangeQuery query)
    {
        var logEntries = await _logs
            .Where(log => 
            log.TrackingTime.CompareTo(query.StartTime) >= 0 &&
            log.TrackingTime.CompareTo(query.EndTime) <= 0)
            .Skip((query.Page - 1) * query.ItemsPerPage)
            .Take(query.ItemsPerPage)
            .ToListAsync();

        return _mapper.Map<IEnumerable<InventoryLogEntry>, IEnumerable<InventoryLogEntryViewModel>>(logEntries);
    }

    public async Task<ExtendedInventoryLogEntryViewModel> GetEntryByItem(TimeRangeQuery query, string itemId, string unit)
    {
        double beforeQuantity, receivedQuantity, shippedQuantity, afterQuantity;
        Item? item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == itemId && i.Unit == unit);
        if (item is null)
        {
            throw new EntityNotFoundException($"Item with Id {itemId} & unit {unit} not found.");
        }
        var itemViewModel = _mapper.Map<Item, ItemViewModel>(item); 

        var logEntries = await _logs
            .Where(log =>
            log.TrackingTime.CompareTo(query.StartTime) >= 0 &&
            log.TrackingTime.CompareTo(query.EndTime) <= 0)
            .Where(log => log.Item.ItemId == itemId)
            .Where(log => log.Item.Unit == unit)           
            .ToListAsync();

        if (logEntries.Count != 0)
        {
            beforeQuantity = logEntries[0].BeforeQuantity;
            afterQuantity = logEntries[^1].BeforeQuantity + logEntries[^1].ChangedQuantity;
            receivedQuantity = logEntries
                .Sum(log => log.ReceivedQuantity);
            shippedQuantity = logEntries
                .Sum(log => log.ShippedQuantity);
        }
        else
        {
            var latestEntry = await _context.InventoryLogEntries
                .Where(log => log.Item.ItemId == itemId)
                .Where(log => log.Item.Unit == unit)
                .OrderByDescending(log => log.TrackingTime)               
                .FirstOrDefaultAsync(log => log.TrackingTime < query.StartTime);
            if (latestEntry is null)
            {
                beforeQuantity = 0;
                afterQuantity = 0;
                receivedQuantity = 0;
                shippedQuantity = 0;
            }
            else
            {
                beforeQuantity = latestEntry.BeforeQuantity + latestEntry.ChangedQuantity;
                receivedQuantity = shippedQuantity = 0;
                afterQuantity = beforeQuantity;
            }           
        }

        var extendedLogEntry = new ExtendedInventoryLogEntryViewModel(itemViewModel, beforeQuantity, receivedQuantity, shippedQuantity, afterQuantity);
        return extendedLogEntry;
    }

    public async Task<IEnumerable<ExtendedInventoryLogEntryViewModel>> GetExtendedLogEntries(TimeRangeQuery query, string? itemClassId, string? itemId)
    {
        List<Item> items;
        IQueryable<Item> itemsQuery = _context.Items
            .AsNoTracking()
            .Skip(query.ItemsPerPage * (query.Page - 1))
            .Take(query.ItemsPerPage);

        if (itemId == null && itemClassId != null)
        {
            items = await itemsQuery
                .Where(i => i.ItemClassId == itemClassId)
                .Skip((query.Page - 1) * query.ItemsPerPage)
                .Take(query.ItemsPerPage)
                .ToListAsync();
        }
        else if (itemId != null && itemClassId == null)
        {
            items = await itemsQuery
                .Where(i => i.ItemId == itemId)
                .ToListAsync();
        }
        else if (itemId != null && itemClassId != null)
        {
            items = await itemsQuery
                .Where(i => i.ItemClassId == itemClassId && i.ItemId == itemId)
                .ToListAsync();
        }
        else
            throw new EntityNotFoundException("Invalid Request");

        var extendedLogEntries = new List<ExtendedInventoryLogEntryViewModel>();
        foreach (var item in items)
        {
            var entry = await GetEntryByItem(query, item.ItemId, item.Unit);
            if (entry.BeforeQuantity == 0 && entry.ReceivedQuantity == 0 && entry.ShippedQuantity == 0)
                continue;
            extendedLogEntries.Add(entry);
        }
        return extendedLogEntries;
    }

    public async Task<ItemLogEntryViewModel> GetItemLotsLogEntry(DateTime trackingTime, string itemId)
    {

        var filteredLogEntries = await _logs
            .Where(log => log.Item.ItemId == itemId)
            .Where(log => log.TrackingTime.CompareTo(trackingTime) <= 0)
            .ToListAsync();

        if (filteredLogEntries.Count == 0)
            throw new EntityNotFoundException($"Could not found itemlots with itemId {itemId} in this timerange.");

        List<ItemLotLogEntryViewModel> lotViewModels = new ();

        var groupEntries = filteredLogEntries.GroupBy(log => log.ItemLotId);
        ItemViewModel item = _mapper.Map<ItemViewModel>(groupEntries.First().First().Item);
        foreach (var groupEntry in groupEntries)
        {
            string? itemLotId = groupEntry.Key;
            if (itemLotId == null)
                continue;

            double totalLotQuantity = groupEntry.Sum(entry => entry.ChangedQuantity);

            double? numOfPackets;
            if (item.PacketSize > 0)
            {
                numOfPackets = Math.Round((double)(totalLotQuantity / item.PacketSize), 2);
            }
            else
                numOfPackets = null;

            var lotLogEntryVM = new ItemLotLogEntryViewModel(itemLotId, totalLotQuantity, numOfPackets);
            lotViewModels.Add(lotLogEntryVM);
        }
        double totalQuantity = lotViewModels.Sum(i => i.Quantity);
        ItemLogEntryViewModel logEntryViewModel = new (item, totalQuantity, lotViewModels);

        return logEntryViewModel;
    }
}
