namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductInventories;

public class ExtendedProductInventoryLogEntryViewModel
{
    public ItemViewModel Item { get; private set; }
    public double BeforeQuantity { get; private set; }
    public double ReceivedQuantity { get; private set; }
    public double ShippedQuantity { get; private set; }
    public double AfterQuantity { get; private set; }

    public ExtendedProductInventoryLogEntryViewModel(ItemViewModel item, double beforeQuantity, double receivedQuantity, double shippedQuantity, 
        double afterQuantity)
    {
        Item = item;
        BeforeQuantity = beforeQuantity;
        ReceivedQuantity = receivedQuantity;
        ShippedQuantity = shippedQuantity;
        AfterQuantity = afterQuantity;
    }
}
