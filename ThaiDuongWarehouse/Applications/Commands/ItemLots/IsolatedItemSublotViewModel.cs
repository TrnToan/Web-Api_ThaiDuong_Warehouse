namespace ThaiDuongWarehouse.Api.Applications.Commands.ItemLots;

public class IsolatedItemSublotViewModel
{
    public string LocationId { get; private set; }
    public double Quantity { get; private set; }

    public IsolatedItemSublotViewModel(string locationId, double quantity)
    {
        LocationId = locationId;
        Quantity = quantity;
    }
}
