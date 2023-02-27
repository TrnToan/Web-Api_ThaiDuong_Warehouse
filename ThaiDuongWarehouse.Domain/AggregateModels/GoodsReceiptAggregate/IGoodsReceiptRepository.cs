namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;

public interface IGoodsReceiptRepository : IRepository<GoodsReceipt>
{
    GoodsReceipt Add(GoodsReceipt goodsReceipt);
    void Update(GoodsReceipt goodsReceipt);
    Task<IEnumerable<GoodsReceipt>> GetAll();
    Task<IEnumerable<GoodsReceipt>> GetGoodsReceiptsById(string goodsReceiptId);

}
