﻿namespace ThaiDuongWarehouse.Domain.AggregateModels.ItemLotLocationAggregate;
public class ItemLotLocation : IAggregateRoot
{
    public int ItemLotId { get; private set; }
    public int LocationId { get; private set; }
    public double QuantityPerLocation { get; private set; }
    public ItemLot ItemLot { get; private set; }
    public Location Location { get; private set; }

    public ItemLotLocation(int itemLotId, int locationId, double quantityPerLocation)
    {
        ItemLotId = itemLotId;
        LocationId = locationId;
        QuantityPerLocation = quantityPerLocation;
    }

    public void SetQuantity(double quantity)
    {
        QuantityPerLocation = quantity;
    }

    public void UpdateQuantity(double quantity)
    {
        QuantityPerLocation += quantity;
    }
}
