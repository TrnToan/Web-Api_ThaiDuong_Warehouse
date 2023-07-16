namespace ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;

public interface IInventoryLogEntryQueries
{
    Task<IEnumerable<InventoryLogEntryViewModel>> GetEntriesByItem(string itemId, TimeRangeQuery query);
    Task<IEnumerable<InventoryLogEntryViewModel>> GetEntries(TimeRangeQuery query);
    Task<IEnumerable<ItemLotLogEntryViewModel>> GetItemLotsLogEntry(DateTime trackingTime, string itemId);
    Task<IEnumerable<ExtendedInventoryLogEntryViewModel>> GetExtendedLogEntries(TimeRangeQuery query, string? itemClassId, string? itemId);
}
