namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class StorageRepository : BaseRepository, IStorageRepository
{
    public StorageRepository(WarehouseDbContext context) : base(context)
    {
    }

    public Location Add(Location location)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Warehouse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Warehouse>> GetWarehouseById(int warehouseId)
    {
        throw new NotImplementedException();
    }
}
