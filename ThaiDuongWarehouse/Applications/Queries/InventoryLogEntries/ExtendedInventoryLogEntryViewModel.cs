namespace ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;

public class ExtendedInventoryLogEntryViewModel
{
    public ItemViewModel Item { get; set; }
    public double BeforeQuantity { get; set; }
    public double ReceivedQuantity { get; set; }
    public double ShippedQuantity { get; set; }
    public double AfterQuantity { get; set; }

    public ExtendedInventoryLogEntryViewModel(ItemViewModel item, double beforeQuantity, double receivedQuantity, 
        double shippedQuantity, double afterQuantity)
    {
        Item = item;
        BeforeQuantity = beforeQuantity;
        ReceivedQuantity = receivedQuantity;
        ShippedQuantity = shippedQuantity;
        AfterQuantity = afterQuantity;
    }
}
