namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class UpdateInventoryOnModifyProductReceiptEntryDomainEvent : INotification
{
    public string OldPurchaseOrderNumber { get; private set; }
    public string NewPurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }
    public DateTime Timestamp { get; private set; }
    public Item Item { get; private set; }

    public UpdateInventoryOnModifyProductReceiptEntryDomainEvent(string oldPurchaseOrderNumber, string newPurchaseOrderNumber, 
        double quantity, DateTime timestamp, Item item)
    {
        OldPurchaseOrderNumber = oldPurchaseOrderNumber;
        NewPurchaseOrderNumber = newPurchaseOrderNumber;
        Quantity = quantity;
        Timestamp = timestamp;
        Item = item;
    }
}
