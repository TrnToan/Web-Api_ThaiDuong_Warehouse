namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class InventoryLogEntryChangedDomainEvent : INotification
{
    public string OldItemLotId { get; private set; }
    public string NewItemLotId { get; private set; }
    public int ItemId { get; private set; }
    public DateTime Timestamp { get; private set; }

    public InventoryLogEntryChangedDomainEvent(string oldItemLotId, string newItemLotId, int itemId, DateTime timestamp)
    {
        OldItemLotId = oldItemLotId;
        NewItemLotId = newItemLotId;
        ItemId = itemId;
        Timestamp = timestamp;
    }
}