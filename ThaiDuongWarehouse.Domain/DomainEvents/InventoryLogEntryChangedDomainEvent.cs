namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class InventoryLogEntryChangedDomainEvent : INotification
{
    public string ItemLotId { get; private set; }
    public double Quantity { get; private set; }
    public Item Item { get; private set; }
    public DateTime Timestamp { get; private set; }
    public InventoryLogEntryChangedDomainEvent(string itemLotId, double quantity, Item item, DateTime timestamp)
    {
        ItemLotId = itemLotId;
        Quantity = quantity;
        Item = item;
        Timestamp = timestamp;
    }
}
