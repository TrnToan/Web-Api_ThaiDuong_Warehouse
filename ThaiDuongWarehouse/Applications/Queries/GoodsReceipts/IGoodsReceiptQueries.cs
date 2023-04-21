namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsReceipt;

public interface IGoodsReceiptQueries
{
    Task<IEnumerable<GoodsReceiptViewModel>> GetAll();
    Task<IEnumerable<GoodsReceiptViewModel>> GetConfirmedGoodsReceipt();
    Task<IEnumerable<GoodsReceiptViewModel>> GetUnConfirmedGoodsReceipt();
    Task<IEnumerable<GoodsReceiptViewModel>> GetGoodsReceiptsByTime(TimeRangeQuery query);
    Task<IList<string?>> GetSuppliers();
    Task<IList<string?>> GetPOs();
    Task<GoodsReceiptViewModel?> GetGoodsReceiptById(string goodsReceiptId);   
}
