namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductInventories;

public class FinishedProductInventoryViewModel
{
    public string PurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }
    public ItemViewModel Item { get; private set; }

    public FinishedProductInventoryViewModel(string purchaseOrderNumber, double quantity, ItemViewModel item)
    {
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
        Item = item;
    }
}
