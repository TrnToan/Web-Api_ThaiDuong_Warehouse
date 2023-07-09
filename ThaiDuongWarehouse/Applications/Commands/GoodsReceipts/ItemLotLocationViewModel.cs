namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class ItemLotLocationViewModel
{
    public string LocationId { get; private set; }
    public double QuantityPerLocation { get; private set; }

    public ItemLotLocationViewModel(string locationId, double quantityPerLocation)
    {
        LocationId = locationId;
        QuantityPerLocation = quantityPerLocation;
    }
}
