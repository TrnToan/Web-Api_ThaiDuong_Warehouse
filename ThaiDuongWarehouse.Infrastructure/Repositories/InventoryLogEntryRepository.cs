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

    public void Update(InventoryLogEntry logEntry)
    {
        _context.InventoryLogEntries.Update(logEntry);
    }

    public void UpdateEntries(IEnumerable<InventoryLogEntry> entries)
    {
        _context.InventoryLogEntries.UpdateRange(entries);
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
            .Where(log => log.TrackingTime >= timestamp)
            .Include(log => log.Item)
            .ToListAsync();
    }

    public async Task<InventoryLogEntry?> GetLatestLogEntry(int itemId)
    {
        return await _context.InventoryLogEntries
            .Include(log => log.Item)
            .OrderByDescending(log => log.Id)
            .FirstOrDefaultAsync(log => log.Item.Id == itemId);
    }

    public async Task<InventoryLogEntry?> GetLogEntry(int itemId, string lotId, DateTime timestamp)
    {
        return await _context.InventoryLogEntries
            .Where(log => log.Timestamp == timestamp)
            .SingleOrDefaultAsync(log => log.ItemId == itemId && log.ItemLotId == lotId);
    }

    public async Task<InventoryLogEntry?> GetPreviousLogEntry(int itemId, DateTime trackingTime)
    {
        return await _context.InventoryLogEntries
            .Where(log => log.TrackingTime < trackingTime)
            .OrderBy(log => log.TrackingTime)
            .LastOrDefaultAsync(log => log.ItemId == itemId);
    }

    public async Task<InventoryLogEntry?> GetFinishedProductLogEntry(int itemId, string PO, DateTime timestamp)
    {
        return await _context.InventoryLogEntries
            .Where(log => log.Timestamp == timestamp)
            .SingleOrDefaultAsync(log => log.ItemId == itemId && log.ItemLotId == PO);
    }

    public void Delete(InventoryLogEntry logEntry)
    {
        _context.InventoryLogEntries.Remove(logEntry);
    }
}
