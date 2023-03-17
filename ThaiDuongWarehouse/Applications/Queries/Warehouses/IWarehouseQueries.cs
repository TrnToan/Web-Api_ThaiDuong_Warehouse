namespace ThaiDuongWarehouse.Api.Applications.Queries.Warehouses;

public interface IWarehouseQueries
{
    Task<IEnumerable<WarehouseViewModel>> GetAllWarehouses();
    Task<WarehouseViewModel?> GetWarehouseById(string warehouseId);
}
