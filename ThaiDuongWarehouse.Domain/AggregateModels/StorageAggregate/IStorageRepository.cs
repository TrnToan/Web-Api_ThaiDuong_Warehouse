namespace ThaiDuongWarehouse.Domain.AggregateModels.StorageAggregate
{
    public interface IStorageRepository
    {
        Task<IEnumerable<Warehouse>> GetAll();
        Task<IEnumerable<Warehouse>> GetWarehouseById(int warehouseId);
        Location Add(Location location);
    }
}
