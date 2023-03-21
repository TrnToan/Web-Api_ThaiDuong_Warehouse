using ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsReceipt;

public interface IGoodsReceiptQueries
{
    Task<IEnumerable<GoodsReceiptViewModel>> GetConfirmedGoodsReceipt();
    Task<IEnumerable<GoodsReceiptViewModel>> GetUnConfirmedGoodsReceipt();
    Task<IEnumerable<GoodsReceiptViewModel>> GetGoodsReceiptsByTime(TimeRangeQuery query);
    Task<GoodsReceiptViewModel?> GetGoodsReceiptById(string goodsReceiptId);
}
