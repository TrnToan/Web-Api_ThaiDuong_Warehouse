namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Import;

public interface IImportHistoryQueries
{
    //Task<IEnumerable<GoodsReceiptsHistoryViewModel>> GetByPO(string purchaseOrderNumber);
    Task<IEnumerable<GoodsReceiptHistoryViewModel>> GetBySupplier(TimeRangeQuery query, string supplier);
    Task<IEnumerable<GoodsReceiptHistoryViewModel>> GetByClassOrItem(TimeRangeQuery query, string? itemClassId, string? itemId);
}

