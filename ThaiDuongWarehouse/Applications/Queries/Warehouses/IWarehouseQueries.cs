namespace ThaiDuongWarehouse.Api.Applications.Queries.Warehouses;

public interface IWarehouseQueries
{
    Task<IEnumerable<WarehouseViewModel>> GetAllWarehouses();
    Task<IEnumerable<LocationViewModel>> GetAllLocations();
    Task<WarehouseViewModel?> GetWarehouseById(string warehouseId);
}
