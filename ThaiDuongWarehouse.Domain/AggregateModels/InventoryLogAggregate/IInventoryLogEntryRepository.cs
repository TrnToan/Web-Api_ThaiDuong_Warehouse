namespace ThaiDuongWarehouse.Domain.AggregateModels.InventoryLogAggregate;
public interface IInventoryLogEntryRepository : IRepository<InventoryLogEntry>
{
    Task<IEnumerable<InventoryLogEntry>> GetAll();
    Task<IEnumerable<InventoryLogEntry>> GetByItem(string itemId);
    Task<IEnumerable<InventoryLogEntry>> GetByTime(DateTime startTime, DateTime endTime);
    void Add(InventoryLogEntry logEntry);
}
