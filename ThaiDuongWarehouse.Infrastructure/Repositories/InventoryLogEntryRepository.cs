namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class InventoryLogEntryRepository : BaseRepository, IInventoryLogEntryRepository
{
    public InventoryLogEntryRepository(WarehouseDbContext context) : base(context)
    {
    }

    public void Add(InventoryLogEntry logEntry)
    {
        _context.InventoryLogEntries.AddAsync(logEntry);
    }

    public async Task<IEnumerable<InventoryLogEntry>> GetAll()
    {
        return await _context.InventoryLogEntries
            .OrderByDescending(i => i.Timestamp)
            .ToListAsync();
    }

    public async Task<IEnumerable<InventoryLogEntry>> GetByItem(string itemId)
    {
        return await _context.InventoryLogEntries
            .Where(log => log.Item.ItemId == itemId)
            .ToListAsync();
    }

    public Task<IEnumerable<InventoryLogEntry>> GetByTime(DateTime startTime, DateTime endTime)
    {
        throw new NotImplementedException();
    }

    public async Task<List<InventoryLogEntry>> GetLatestLogEntries(int itemId, DateTime timestamp)
    {
        return await _context.InventoryLogEntries
            .Where(log => log.ItemId == itemId)
            .Where(log => log.Timestamp >= timestamp)
            .Include(log => log.Item)
            .ToListAsync();
    }

    public async Task<InventoryLogEntry?> GetLatestLogEntry(int itemId)
    {
        return await _context.InventoryLogEntries
            .OrderByDescending(log => log.Timestamp)
            .Include(log => log.Item)
            .FirstOrDefaultAsync(log => log.Item.Id == itemId);
    }

    public async Task<InventoryLogEntry?> GetLogEntry(string lotId, DateTime timestamp)
    {
        return await _context.InventoryLogEntries
            .Where(log => log.Timestamp == timestamp)
            .FirstOrDefaultAsync(log => log.ItemLotId == lotId);
    }
}
