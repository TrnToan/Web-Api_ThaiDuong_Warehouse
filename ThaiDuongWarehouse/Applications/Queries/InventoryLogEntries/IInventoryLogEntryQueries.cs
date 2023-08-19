namespace ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;

public interface IInventoryLogEntryQueries
{
    Task<IEnumerable<InventoryLogEntryViewModel>> GetEntriesByItem(string itemId, TimeRangeQuery query);
    Task<IEnumerable<InventoryLogEntryViewModel>> GetEntries(TimeRangeQuery query);
    Task<ItemLogEntryViewModel> GetItemLotsLogEntry(DateTime trackingTime, string itemId);
    Task<QueryResult<ExtendedInventoryLogEntryViewModel>> GetExtendedLogEntries(TimeRangeQuery query, string? itemClassId, string? itemId);
}
