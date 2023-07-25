namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Export;

public interface IExportHistoryQueries
{
    //Task<IEnumerable<GoodsIssuesHistoryViewModel>> GetByPO(string purchaseOrderNumber);
    Task<IEnumerable<GoodsIssueHistoryViewModel>> GetByReceiver(TimeRangeQuery query, string receiver);
    Task<IEnumerable<GoodsIssueHistoryViewModel>> GetByClassOrItem(TimeRangeQuery query, string? itemClassId, string? itemId);
}
