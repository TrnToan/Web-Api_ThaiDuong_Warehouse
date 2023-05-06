namespace ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;

public interface IInventoryLogEntryQueries
{
    Task<IEnumerable<InventoryLogEntryViewModel>> GetAll();
    Task<IEnumerable<InventoryLogEntryViewModel>> GetEntriesByItem(string itemId, TimeRangeQuery query);
    Task<IEnumerable<InventoryLogEntryViewModel>> GetByTime(TimeRangeQuery query);
    Task<ExtendedInventoryLogEntryViewModel> GetEntryByItem(TimeRangeQuery query, string itemId, string unit);
    Task<IEnumerable<ExtendedInventoryLogEntryViewModel>> GetEntriesByItemClass(TimeRangeQuery query, string itemClassId);
}
