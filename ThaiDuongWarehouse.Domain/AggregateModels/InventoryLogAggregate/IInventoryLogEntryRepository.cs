namespace ThaiDuongWarehouse.Domain.AggregateModels.InventoryLogAggregate;
public interface IInventoryLogEntryRepository : IRepository<InventoryLogEntry>
{
    Task<IEnumerable<InventoryLogEntry>> GetByItem(string itemId);
    Task<IEnumerable<InventoryLogEntry>> GetByTime(DateTime startTime, DateTime endTime);
    Task<List<InventoryLogEntry>> GetLatestLogEntries(int itemId, DateTime timestamp);
    Task<InventoryLogEntry?> GetLatestLogEntry(int itemId);
    Task<InventoryLogEntry?> GetLogEntry(string lotId, DateTime timestamp);
    Task<InventoryLogEntry?> GetPreviousLogEntry(int itemId, DateTime trackingTime);
    void Add(InventoryLogEntry logEntry);
    void Update(InventoryLogEntry logEntry);
    void UpdateEntries(IEnumerable<InventoryLogEntry> entries);
    void Delete(InventoryLogEntry logEntry);
}
