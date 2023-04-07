namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Import;

public interface IImportHistoryQueries
{
    Task<IEnumerable<GoodsReceiptsHistoryViewModel>> GetByPO(string purchaseOrderNumber);
    Task<IEnumerable<GoodsReceiptsHistoryViewModel>> GetBySupplier(TimeRangeQuery query, string supplier);
    Task<IEnumerable<GoodsReceiptsHistoryViewModel>> GetByClassOrItem(TimeRangeQuery query, string? itemClassId, string? itemId);
}

