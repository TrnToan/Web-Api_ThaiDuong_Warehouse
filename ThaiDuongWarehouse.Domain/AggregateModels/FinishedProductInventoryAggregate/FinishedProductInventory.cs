namespace ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate;
public class FinishedProductInventory : Entity, IAggregateRoot
{
    public string PurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }
    public DateTime Timestamp { get; private set; }
    public Item Item { get; private set; }

    public FinishedProductInventory(string purchaseOrderNumber, double quantity, DateTime timestamp)
    {
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
        Timestamp = timestamp;
    }

    public FinishedProductInventory(string purchaseOrderNumber, double quantity, DateTime timestamp, Item item) : this(purchaseOrderNumber, quantity, timestamp)
    {
        Item = item;  
    }

    public void UpdateProductInventory(string purchaseOrderNumber, double quantity)
    {
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
    }
}
