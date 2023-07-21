namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class DeleteInventoryLogEntryDomainEvent : INotification
{
    public int ItemId { get; private set; }
    public string ItemLotId { get; private set; }
    public DateTime Timestamp { get; private set; }

    public DeleteInventoryLogEntryDomainEvent(int itemId, string itemLotId, DateTime timestamp)
    {
        ItemId = itemId;
        ItemLotId = itemLotId;
        Timestamp = timestamp;
    }
}
