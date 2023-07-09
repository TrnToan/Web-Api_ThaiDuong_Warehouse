namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductReceipts;

public class FinishedProductReceiptEntryViewModel
{
    public string PurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }
    public string? Note { get; private set; }
    public ItemViewModel Item { get; private set; }

    public FinishedProductReceiptEntryViewModel(string purchaseOrderNumber, double quantity, string? note, ItemViewModel item)
    {
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
        Note = note;
        Item = item;
    }
}
