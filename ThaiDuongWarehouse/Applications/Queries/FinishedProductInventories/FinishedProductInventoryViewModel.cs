namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductInventories;

public class FinishedProductInventoryViewModel
{
    public string PurchaseOrderNumber { get; set; }
    public double Quantity { get; set; }
    public ItemViewModel Item { get; set; }
    public double? NumOfPackets { get; set; }

    public FinishedProductInventoryViewModel(string purchaseOrderNumber, double quantity, ItemViewModel item)
    {
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
        Item = item;
        if (item.PacketSize > 0)
        {
            NumOfPackets = Quantity / item.PacketSize;
        }
        else
        {
            NumOfPackets = null;
        }
    }
}
