namespace ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate;
public class FinishedProductInventory : Entity, IAggregateRoot
{
    public string PurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }
    public Item Item { get; private set; }

    public FinishedProductInventory(string purchaseOrderNumber, double quantity)
    {
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
    }

    public FinishedProductInventory(string purchaseOrderNumber, double quantity, Item item) : this(purchaseOrderNumber, quantity)
    {
        Item = item;  
    }

    public void UpdateQuantity(double quantity)
    {
        Quantity += quantity;
    }

    public void SetQuantity(double quantity)
    {
        Quantity = quantity;
    }
}
