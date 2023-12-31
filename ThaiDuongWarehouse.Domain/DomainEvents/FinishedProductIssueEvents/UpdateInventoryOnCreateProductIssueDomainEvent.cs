namespace ThaiDuongWarehouse.Domain.DomainEvents.FinishedProductIssueEvents;
public class UpdateInventoryOnCreateProductIssueDomainEvent : INotification
{
    public Item Item { get; private set; }
    public string PurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }

    public UpdateInventoryOnCreateProductIssueDomainEvent(Item item, string purchaseOrderNumber, double quantity)
    {
        Item = item;
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
    }
}
