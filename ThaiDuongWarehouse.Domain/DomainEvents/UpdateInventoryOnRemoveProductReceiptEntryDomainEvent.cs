namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class UpdateInventoryOnRemoveProductReceiptEntryDomainEvent : INotification
{
    public Item Item { get; private set; }
    public string PurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }

    public UpdateInventoryOnRemoveProductReceiptEntryDomainEvent(Item item, string purchaseOrderNumber, double quantity)
    {
        Item = item;
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
    }
}
