namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Export;

public interface IExportHistoryQueries
{
    Task<IEnumerable<GoodsIssuesHistoryViewModel>> GetByPO(string purchaseOrderNumber);
    Task<IEnumerable<GoodsIssuesHistoryViewModel>> GetByReceiver(TimeRangeQuery query, string receiver);
    Task<IEnumerable<GoodsIssuesHistoryViewModel>> GetByClassOrItem(TimeRangeQuery query, string? itemClassId, string? itemId);
}
