namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class ItemLotRepository : BaseRepository, IItemLotRepository
{
    public ItemLotRepository(WarehouseDbContext context) : base(context)
    {
    }

    public IUnitOfWork unitOfWork => throw new NotImplementedException();

    public void AddLot(ItemLot itemLot)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ItemLot>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ItemLot>> GetLotByItemId(string itemId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ItemLot>> GetLotByLotId(string lotId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ItemLot>> GetLotByPO(string purchaseOrderNumber)
    {
        throw new NotImplementedException();
    }

    public void UpdateLot(ItemLot itemLot)
    {
        throw new NotImplementedException();
    }
}
