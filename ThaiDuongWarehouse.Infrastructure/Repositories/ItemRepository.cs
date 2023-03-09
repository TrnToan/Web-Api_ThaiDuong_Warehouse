namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class ItemRepository : BaseRepository, IItemRepository
{
    public ItemRepository(WarehouseDbContext context) : base(context)
    {
    }

    public IUnitOfWork unitOfWork => throw new NotImplementedException();

    public Item Add(Item item)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Item>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Item?> GetItemById(string itemId)
    {
        throw new NotImplementedException();
    }

    public Item Update(Item item)
    {
        throw new NotImplementedException();
    }
}
