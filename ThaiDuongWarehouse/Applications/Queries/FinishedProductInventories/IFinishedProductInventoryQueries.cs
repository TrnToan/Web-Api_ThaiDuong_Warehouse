namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductInventories;

public interface IFinishedProductInventoryQueries
{
    Task<IEnumerable<FinishedProductInventoryViewModel>> GetProductInventoriesByItemId(string itemId);
    Task<IEnumerable<string>> GetPOs();
    Task<ExtendedProductInventoryLogEntryViewModel> GetProductInventoryLog(string itemId, string unit, TimeRangeQuery query);
    Task<IEnumerable<ExtendedProductInventoryLogEntryViewModel>> GetProductInventoryLogs(TimeRangeQuery query);
}
