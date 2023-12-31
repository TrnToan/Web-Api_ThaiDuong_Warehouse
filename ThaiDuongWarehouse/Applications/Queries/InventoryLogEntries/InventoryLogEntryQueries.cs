using System.Diagnostics;

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

    private async Task<ExtendedInventoryLogEntryViewModel> GetEntryByItem(List<InventoryLogEntry> logEntries,  
        Item item, TimeRangeQuery query)
    {
        double beforeQuantity, receivedQuantity, shippedQuantity, afterQuantity;        
        var itemViewModel = _mapper.Map<Item, ItemViewModel>(item);

        var sortedLogEntries = logEntries
            .OrderBy(log => log.TrackingTime);

        var filteredLogEntries = sortedLogEntries
            .Where(log =>
            log.TrackingTime >= query.StartTime &&
            log.TrackingTime <= query.EndTime);

        if (filteredLogEntries.Any())
        {
            beforeQuantity = filteredLogEntries.First().BeforeQuantity;
            afterQuantity = filteredLogEntries.Last().BeforeQuantity + filteredLogEntries.Last().ChangedQuantity;
            receivedQuantity = filteredLogEntries
                .Sum(log => log.ReceivedQuantity);
            shippedQuantity = filteredLogEntries
                .Sum(log => log.ShippedQuantity);
        }
        else
        {
            var latestEntry = sortedLogEntries
                .LastOrDefault(log => log.TrackingTime < query.StartTime);

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

        var extendedLogEntry = new ExtendedInventoryLogEntryViewModel(itemViewModel, beforeQuantity, receivedQuantity, 
            shippedQuantity, afterQuantity);
        return extendedLogEntry;
    }

    public async Task<QueryResult<ExtendedInventoryLogEntryViewModel>> GetExtendedLogEntries(TimeRangeQuery query, string? itemClassId, string? itemId)
    {
        IQueryable<Item> items = _context.Items
            .AsNoTracking();

        if (itemId == null && itemClassId != null)
        {
            items = items
                .Where(i => i.ItemClassId == itemClassId);
        }
        else if (itemId != null && itemClassId == null)
        {
            items = items
                .Where(i => i.ItemId == itemId);
        }
        else if (itemId != null && itemClassId != null)
        {
            items = items
                .Where(i => i.ItemClassId == itemClassId && i.ItemId == itemId);
        }
        else
            throw new EntityNotFoundException("Invalid Request");

        var logEntries = await _logs
            .GroupBy(log => log.Item.Id)
            .ToDictionaryAsync(gr => gr.Key, gr => gr.ToList());

        var extendedLogEntries = new List<ExtendedInventoryLogEntryViewModel>();
        foreach (var item in items)
        {
            if (logEntries.TryGetValue(item.Id, out List<InventoryLogEntry>? itemLogEntries))
            {
                ExtendedInventoryLogEntryViewModel entry = await GetEntryByItem(itemLogEntries, item, query);
                extendedLogEntries.Add(entry);
            }             
        }

        int totalNumOfEntries = extendedLogEntries.Count;
        extendedLogEntries = extendedLogEntries
            .Skip((query.Page - 1) * query.ItemsPerPage)
            .Take(query.ItemsPerPage)
            .ToList();

        QueryResult<ExtendedInventoryLogEntryViewModel> viewModels = new (extendedLogEntries, totalNumOfEntries);
        return viewModels;
    }

    public async Task<ItemLogEntryViewModel> GetItemLotsLogEntry(DateTime trackingTime, string itemId)
    {
        var filteredLogEntries = await _logs
            .Where(log => log.Item.ItemId == itemId)
            .Where(log => log.TrackingTime.CompareTo(trackingTime) <= 0)
            .ToListAsync();

        List<ItemLotLogEntryViewModel> lotViewModels = new();

        if (filteredLogEntries.Count == 0)
        {
            Item? existedItem = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == itemId);
            ItemViewModel itemVM = _mapper.Map<ItemViewModel>(existedItem);             
            ItemLogEntryViewModel viewModel = new (itemVM, 0, lotViewModels);
            return viewModel;
        }

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
