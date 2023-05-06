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

    public async Task<IEnumerable<InventoryLogEntryViewModel>> GetAll()
    {
        var logEntries = await _context.InventoryLogEntries
            .AsNoTracking()
            .Include(log => log.Item)
            .ToListAsync();

        return _mapper.Map<IEnumerable<InventoryLogEntry>, IEnumerable<InventoryLogEntryViewModel>>(logEntries);
    }

    public async Task<IEnumerable<InventoryLogEntryViewModel>> GetEntriesByItem(string itemId, TimeRangeQuery query)
    {
        var logEntries = await _context.InventoryLogEntries
            .AsNoTracking()
            .Include(log => log.Item)
            .Where(log => log.Item.ItemId == itemId)
            .Where(log =>
            log.Timestamp.CompareTo(query.StartTime) > 0 &&
            log.Timestamp.CompareTo(query.EndTime) < 0)
            .ToListAsync();

        return _mapper.Map<IEnumerable<InventoryLogEntry>, IEnumerable<InventoryLogEntryViewModel>>(logEntries);
    }

    public async Task<IEnumerable<InventoryLogEntryViewModel>> GetByTime(TimeRangeQuery query)
    {
        var logEntries = await _context.InventoryLogEntries
            .AsNoTracking()
            .Include(log => log.Item)
            .Where(log => 
            log.Timestamp.CompareTo(query.StartTime) > 0 &&
            log.Timestamp.CompareTo(query.EndTime) < 0)
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
            log.Timestamp.CompareTo(query.StartTime) >= 0 &&
            log.Timestamp.CompareTo(query.EndTime) <= 0)
            .Where(log => log.Item.ItemId == itemId)
            .Where(log => log.Item.Unit == unit)           
            .ToListAsync();

        if (logEntries.Count != 0)
        {
            beforeQuantity = logEntries[0].BeforeQuantity;
            afterQuantity = logEntries[^1].BeforeQuantity + logEntries[^1].ChangedQuantity;
            receivedQuantity = logEntries
                .Where(log => log.ChangedQuantity > 0)
                .Sum(log => log.ChangedQuantity);
            shippedQuantity = logEntries
                .Where(log => log.ChangedQuantity < 0)
                .Sum(log => log.ChangedQuantity);
        }
        else
        {
            var latestEntry = await _context.InventoryLogEntries
                .Where(log => log.Item.ItemId == itemId)
                .Where(log => log.Item.Unit == unit)
                .OrderByDescending(log => log.Timestamp)               
                .FirstOrDefaultAsync(log => log.Timestamp <= query.StartTime);
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

    public async Task<IEnumerable<ExtendedInventoryLogEntryViewModel>> GetEntriesByItemClass(TimeRangeQuery query, string itemClassId)
    {
        List<Item> items = await _context.Items
            .AsNoTracking()
            .Where(i => i.ItemClassId == itemClassId)
            .ToListAsync();

        var extendedLogEntries = new List<ExtendedInventoryLogEntryViewModel>();
        foreach (var item in items)
        {
            var entry = await this.GetEntryByItem(query, item.ItemId, item.Unit);
            extendedLogEntries.Add(entry);
        }
        return extendedLogEntries;
    }
}
