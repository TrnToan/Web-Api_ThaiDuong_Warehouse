namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class InventoryLogEntryChangedDomainEvent : INotification
{
    public string ItemLotId { get; private set; }
    public double Quantity { get; private set; }
    public int ItemId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public InventoryLogEntryChangedDomainEvent(string itemLotId, double quantity, int itemId, DateTime timestamp)
    {
        ItemLotId = itemLotId;
        Quantity = quantity;
        ItemId = itemId;
        Timestamp = timestamp;
    }
}
