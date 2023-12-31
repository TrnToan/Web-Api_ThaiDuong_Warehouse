namespace ThaiDuongWarehouse.Domain.AggregateModels.InventoryLogAggregate;
public interface IInventoryLogEntryRepository : IRepository<InventoryLogEntry>
{
    Task<List<InventoryLogEntry>> GetLatestLogEntries(int itemId, DateTime timestamp);
    Task<InventoryLogEntry?> GetLatestLogEntry(int itemId);
    Task<InventoryLogEntry?> GetLogEntry(int itemId, string lotId, DateTime timestamp);
    Task<InventoryLogEntry?> GetPreviousLogEntry(int itemId, DateTime trackingTime);
    Task AddAsync(InventoryLogEntry logEntry);
    void Update(InventoryLogEntry logEntry);
    void UpdateEntries(IEnumerable<InventoryLogEntry> entries);
    void Delete(InventoryLogEntry logEntry);
}
