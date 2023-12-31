namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;

public interface IGoodsReceiptRepository : IRepository<GoodsReceipt>
{
    GoodsReceipt Add(GoodsReceipt goodsReceipt);
    void Update(GoodsReceipt goodsReceipt);
    void Remove(GoodsReceipt goodsReceipt);
    Task<GoodsReceipt?> GetGoodsReceiptById(int id);
    Task<GoodsReceipt?> GetGoodsReceiptByGoodsReceiptId(string goodsReceiptId);
    Task<GoodsReceiptLot?> GetGoodsReceiptLotById(string goodsReceiptLotId);
}
