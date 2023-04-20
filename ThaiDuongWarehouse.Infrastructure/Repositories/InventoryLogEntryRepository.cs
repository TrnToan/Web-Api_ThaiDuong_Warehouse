
using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class InventoryLogEntryRepository : BaseRepository, IInventoryLogEntryRepository
{
    public InventoryLogEntryRepository(WarehouseDbContext context) : base(context)
    {
    }

    public void Add(InventoryLogEntry logEntry)
    {
        _context.InventoryLogEntries.Add(logEntry);
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

    public async Task<InventoryLogEntry?> GetLatestLogEntry(int itemId)
    {
        return await _context.InventoryLogEntries
            .OrderByDescending(log => log.Timestamp)
            .Include(log => log.Item)
            .FirstOrDefaultAsync(log => log.Item.Id == itemId);
    }
}
