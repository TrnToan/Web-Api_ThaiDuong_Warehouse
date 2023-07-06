namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class AddFinishedProductInventoryDomainEvent : INotification
{
    public string PurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }
    public DateTime Timestamp { get; private set; }
    public Item Item { get; private set; }

    public AddFinishedProductInventoryDomainEvent(string purchaseOrderNumber, double quantity, DateTime timestamp, Item item)
    {
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
        Timestamp = timestamp;
        Item = item;
    }
}
