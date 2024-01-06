namespace ThaiDuongWarehouse.Api.Applications.Commands.IsolatedItemLots;

public class UnisolatedItemSublotViewModel
{
    public string LocationId { get; private set; }
    public double QuantityPerLocation { get; private set; }

    public UnisolatedItemSublotViewModel(string locationId, double quantityPerLocation)
    {
        LocationId = locationId;
        QuantityPerLocation = quantityPerLocation;
    }
}
