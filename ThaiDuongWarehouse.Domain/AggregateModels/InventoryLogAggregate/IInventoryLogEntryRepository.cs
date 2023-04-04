namespace ThaiDuongWarehouse.Domain.AggregateModels.InventoryLogAggregate;
public interface IInventoryLogEntryRepository : IRepository<InventoryLogEntry>
{
    Task<IEnumerable<InventoryLogEntry>> GetAll();
    Task<IEnumerable<InventoryLogEntry>> GetByItem(string itemId);
    Task<IEnumerable<InventoryLogEntry>> GetByTime(DateTime startTime, DateTime endTime);
    Task<InventoryLogEntry?> GetLatestLogEntry(int itemId);
    void Add(InventoryLogEntry logEntry);
}
