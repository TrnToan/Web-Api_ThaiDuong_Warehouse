using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;

public class InventoryLogEntryQueries : IInventoryLogEntryQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;
    public InventoryLogEntryQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InventoryLogEntryViewModel>> GetEntriesByItem(string itemId, TimeRangeQuery query)
    {
        var logEntries = await _context.InventoryLogEntries
            .AsNoTracking()
            .Include(log => log.Item)
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
        var logEntries = await _context.InventoryLogEntries
            .AsNoTracking()
            .Include(log => log.Item)
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

        var logEntries = await _context.InventoryLogEntries
            .AsNoTracking()
            .Include(log => log.Item)
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
                .FirstOrDefaultAsync(log => log.TrackingTime <= query.StartTime);
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
            .AsNoTracking();

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
            extendedLogEntries.Add(entry);
        }
        return extendedLogEntries;
    }

    public async Task<IEnumerable<ItemLotLogEntryViewModel>> GetItemLotsLogEntry(DateTime trackingTime, string itemId)
    {

        var filteredLogEntries = await _context.InventoryLogEntries
            .AsNoTracking()
            .Include(log => log.Item)
            .Where(log => log.Item.ItemId == itemId)
            .Where(log => log.TrackingTime.CompareTo(trackingTime) >= 0)
            .ToListAsync();

        List<ItemLotLogEntryViewModel> viewModels = new ();
        var groupEntries = filteredLogEntries.GroupBy(log => log.ItemLotId);
        foreach (var groupEntry in groupEntries)
        {
            string? itemLotId = groupEntry.Key;
            double totalQuantity = groupEntry.Sum(entry => entry.ChangedQuantity);

            ItemViewModel item = _mapper.Map<ItemViewModel>(groupEntry.First().Item);
            var lotLogEntryVM = new ItemLotLogEntryViewModel(itemLotId, totalQuantity, item);
            viewModels.Add(lotLogEntryVM);
        }

        return viewModels;
    }
}
