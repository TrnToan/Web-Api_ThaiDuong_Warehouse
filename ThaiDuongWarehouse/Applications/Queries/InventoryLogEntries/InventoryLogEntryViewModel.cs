namespace ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;

public class InventoryLogEntryViewModel
{
    public string ItemLotId { get; private set; }   
    public double BeforeQuantity { get; private set; }
    public double ChangedQuantity { get; private set; }
    public double ReceivedQuantity { get; private set; }
    public double ShippedQuantity { get; private set; }
    public ItemViewModel Item { get; private set; }
    public DateTime Timestamp { get; private set; }
    public DateTime TrackingTime { get; private set; }

    public InventoryLogEntryViewModel(string itemLotId, double beforeQuantity, double changedQuantity, double receivedQuantity, 
        double shippedQuantity, ItemViewModel item, DateTime timestamp, DateTime trackingTime)
    {
        ItemLotId = itemLotId;
        BeforeQuantity = beforeQuantity;
        ChangedQuantity = changedQuantity;
        ReceivedQuantity = receivedQuantity;
        ShippedQuantity = shippedQuantity;
        Item = item;
        Timestamp = timestamp;
        TrackingTime = trackingTime;
    }
}
