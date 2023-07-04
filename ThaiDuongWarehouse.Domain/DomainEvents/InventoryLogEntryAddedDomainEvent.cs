namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class InventoryLogEntryAddedDomainEvent : INotification
{
    public string ItemLotId { get; private set; }
    public double Quantity { get; private set; }
    public double ReceivedQuantity { get; private set; }
    public double ShippedQuantity { get; private set; }
    public int ItemId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public InventoryLogEntryAddedDomainEvent(string itemLotId, double quantity, double receivedQuantity, double shippedQuantity, 
        int itemId, DateTime timestamp)
    {
        ItemLotId = itemLotId;
        Quantity = quantity; 
        ReceivedQuantity = receivedQuantity;
        ShippedQuantity = shippedQuantity;
        ItemId = itemId;
        Timestamp = timestamp;
    }
}
