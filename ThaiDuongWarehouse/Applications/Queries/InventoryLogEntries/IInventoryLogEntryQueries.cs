namespace ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;

public interface IInventoryLogEntryQueries
{
    Task<IEnumerable<InventoryLogEntryViewModel>> GetAll();
    Task<IEnumerable<InventoryLogEntryViewModel>> GetByItem(string itemId);
    Task<IEnumerable<InventoryLogEntryViewModel>> GetByTime(TimeRangeQuery query);
}
