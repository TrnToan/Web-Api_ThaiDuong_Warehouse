namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class UpdateInventoryLogEntriesDomainEvent : INotification
{
    public string ItemLotId { get; private set; }
    public int ItemId { get; private set; }
    public double ChangedQuantity { get; private set; }
    public double ReceivedQuantity { get; private set; }
    public double ShippedQuantity { get; private set; }
    public DateTime Timestamp { get; private set; }

    public UpdateInventoryLogEntriesDomainEvent(string itemLotId, double changedQuantity, double receivedQuantity, 
        double shippedQuantity, int itemId, DateTime timestamp)
    {
        ItemLotId = itemLotId;
        ItemId = itemId;
        ChangedQuantity = changedQuantity;
        Timestamp = timestamp;
        ReceivedQuantity = receivedQuantity;
        ShippedQuantity = shippedQuantity;
    }
}