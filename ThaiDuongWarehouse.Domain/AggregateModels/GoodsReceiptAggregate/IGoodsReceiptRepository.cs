namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;

public interface IGoodsReceiptRepository : IRepository<GoodsReceipt>
{
    GoodsReceipt Add(GoodsReceipt goodsReceipt);
    void Update(GoodsReceipt goodsReceipt);
    void Remove(GoodsReceipt goodsReceipt);
    Task<GoodsReceipt?> GetGoodsReceiptById(string goodsReceiptId);
}
