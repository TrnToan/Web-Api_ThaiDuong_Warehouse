namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class RemoveInventoryOnRemoveProductReceiptEntryDomainEvent : INotification
{
    public Item Item { get; private set; }
    public string PurchaseOrderNumber { get; private set; }
    public DateTime Timestamp { get; private set; }

    public RemoveInventoryOnRemoveProductReceiptEntryDomainEvent(Item item, string purchaseOrderNumber, DateTime timestamp)
    {
        Item = item;
        PurchaseOrderNumber = purchaseOrderNumber;
        Timestamp = timestamp;
    }
}
