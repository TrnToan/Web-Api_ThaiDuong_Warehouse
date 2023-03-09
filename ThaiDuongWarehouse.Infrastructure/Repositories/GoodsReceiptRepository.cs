namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class GoodsReceiptRepository : BaseRepository, IGoodsReceiptRepository
{
    public GoodsReceiptRepository(WarehouseDbContext context) : base(context)
    {
    }

    public IUnitOfWork unitOfWork => throw new NotImplementedException();

    public GoodsReceipt Add(GoodsReceipt goodsReceipt)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GoodsReceipt>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GoodsReceipt>> GetGoodsReceiptsById(string goodsReceiptId)
    {
        throw new NotImplementedException();
    }

    public void Update(GoodsReceipt goodsReceipt)
    {
        throw new NotImplementedException();
    }
}
