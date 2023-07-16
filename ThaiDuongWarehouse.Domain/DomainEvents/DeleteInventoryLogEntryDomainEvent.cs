namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class DeleteInventoryLogEntryDomainEvent : INotification
{
    public string ItemLotId { get; private set; }
    public DateTime Timestamp { get; private set; }

    public DeleteInventoryLogEntryDomainEvent(string itemLotId, DateTime timestamp)
    {
        ItemLotId = itemLotId;
        Timestamp = timestamp;
    }
}
