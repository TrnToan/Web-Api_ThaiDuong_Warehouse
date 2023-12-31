namespace ThaiDuongWarehouse.Domain.DomainEvents.FinishedProductIssueEvents;
public class UpdateInventoryOnRemoveProductIssueEntryDomainEvent : INotification
{
    public Item Item { get; private set; }
    public string PurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }

    public UpdateInventoryOnRemoveProductIssueEntryDomainEvent(Item item, string purchaseOrderNumber, double quantity)
    {
        Item = item;
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
    }
}
