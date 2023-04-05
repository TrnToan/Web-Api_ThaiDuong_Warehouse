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

    public async Task<IEnumerable<InventoryLogEntryViewModel>> GetByItem(string itemId, TimeRangeQuery query)
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
}
