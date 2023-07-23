namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class UpdateInventoryOnModifyProductReceiptEntryDomainEvent : INotification
{
    public string OldPurchaseOrderNumber { get; private set; }
    public string NewPurchaseOrderNumber { get; private set; }
    public double OldQuantity { get; private set; }
    public double NewQuantity { get; private set; }
    public DateTime Timestamp { get; private set; }
    public Item Item { get; private set; }

    public UpdateInventoryOnModifyProductReceiptEntryDomainEvent(string oldPurchaseOrderNumber, string newPurchaseOrderNumber,
         double oldQuantity, double newQuantity, DateTime timestamp, Item item)
    {
        OldPurchaseOrderNumber = oldPurchaseOrderNumber;
        NewPurchaseOrderNumber = newPurchaseOrderNumber;
        NewQuantity = newQuantity;
        Timestamp = timestamp;
        Item = item;
        OldQuantity = oldQuantity;
    }
}
