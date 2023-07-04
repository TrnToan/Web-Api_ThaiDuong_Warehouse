namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsReceipt;

public interface IGoodsReceiptQueries
{
    Task<IEnumerable<GoodsReceiptViewModel>> GetAll();
    Task<IEnumerable<GoodsReceiptViewModel>> GetCompletedGoodsReceipts();
    Task<IEnumerable<GoodsReceiptViewModel>> GetUnCompletedGoodsReceipts();
    Task<IEnumerable<GoodsReceiptViewModel>> GetGoodsReceiptsByTime(TimeRangeQuery query, bool isCompleted);
    Task<IList<string?>> GetSuppliers();
    Task<GoodsReceiptViewModel?> GetGoodsReceiptById(string goodsReceiptId);   
}
