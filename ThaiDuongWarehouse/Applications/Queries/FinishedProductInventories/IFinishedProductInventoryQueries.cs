namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductInventories;

public interface IFinishedProductInventoryQueries
{
    Task<IEnumerable<FinishedProductInventoryViewModel>> GetProductInventoriesByItemId(string itemId);
    Task<IEnumerable<string>> GetPOs();
    Task<QueryResult<ExtendedProductInventoryLogEntryViewModel>> GetProductInventoryLogs(TimeRangeQuery query, string? itemId, string? unit);
    Task<IEnumerable<FinishedProductInventoryViewModel>> GetProductInventoryRecords(DateTime timestamp, string itemId, string unit);
}
