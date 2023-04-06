namespace ThaiDuongWarehouse.Domain.AggregateModels.StorageAggregate;
public class Location : Entity
{
    public string LocationId { get; private set; }

    public int WarehouseId { get; private set; }
    public List<ItemLot> ItemLots { get; private set; }

    public Location(string locationId, int warehouseId)
    {
        LocationId = locationId;
        ItemLots = new List<ItemLot>();
        WarehouseId = warehouseId;
    }
}
