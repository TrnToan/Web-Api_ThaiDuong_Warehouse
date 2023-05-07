namespace ThaiDuongWarehouse.Domain.AggregateModels.InventoryLogAggregate;
public interface IInventoryLogEntryRepository : IRepository<InventoryLogEntry>
{
    Task<IEnumerable<InventoryLogEntry>> GetAll();
    Task<IEnumerable<InventoryLogEntry>> GetByItem(string itemId);
    Task<IEnumerable<InventoryLogEntry>> GetByTime(DateTime startTime, DateTime endTime);
    Task<IList<InventoryLogEntry>> GetLatestLogEntries(int itemId, DateTime timestamp);
    Task<InventoryLogEntry?> GetLatestLogEntry(int itemId);
    Task<InventoryLogEntry?> GetLogEntry(string lotId, DateTime timestamp);
    void Add(InventoryLogEntry logEntry);
}
