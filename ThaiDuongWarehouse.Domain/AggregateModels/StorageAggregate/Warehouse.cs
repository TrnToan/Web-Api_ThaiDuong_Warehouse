namespace ThaiDuongWarehouse.Domain.AggregateModels.StorageAggregate
{
    public class Warehouse
    {
        public string WarehouseId { get; private set; }
        public string WarehouseName { get; private set; }
        public List<Location> Locations { get; private set; }

        public Warehouse(string warehouseId, string warehouseName)
        {
            WarehouseId = warehouseId;
            WarehouseName = warehouseName;
            Locations = new List<Location>();
        }
    }
}
