namespace ThaiDuongWarehouse.Domain.AggregateModels.StorageAggregate;
public interface IStorageRepository : IRepository<Warehouse>
{
    Task<IEnumerable<Warehouse>> GetAll();
    Task<Warehouse?> GetWarehouseById(string warehouseId);
    Location Add(Location location);
}
