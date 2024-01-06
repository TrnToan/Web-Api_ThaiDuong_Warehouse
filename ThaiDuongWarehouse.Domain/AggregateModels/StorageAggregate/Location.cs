using ThaiDuongWarehouse.Domain.AggregateModels.ItemLotLocationAggregate;

namespace ThaiDuongWarehouse.Domain.AggregateModels.StorageAggregate;
public class Location : Entity
{
    public string LocationId { get; private set; }
    public int WarehouseId { get; private set; }
    public List<ItemLotLocation> ItemLotLocations { get; private set; }

    public Location(string locationId, int warehouseId)
    {
        LocationId = locationId; 
        ItemLotLocations = new List<ItemLotLocation>();
        WarehouseId = warehouseId;
    }
}
