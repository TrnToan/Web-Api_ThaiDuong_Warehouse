namespace ThaiDuongWarehouse.Api.Applications.Queries.Warehouses;

public class WarehouseViewModel
{
    public string WarehouseId { get; private set; }
    public string WarehouseName { get; private set; }
    public List<LocationViewModel> Locations { get; private set; }

    public WarehouseViewModel(string warehouseId, string warehouseName, List<LocationViewModel> locations)
    {
        WarehouseId = warehouseId;
        WarehouseName = warehouseName;
        Locations = locations;
    }
}
