namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

public class CreateFinishedProductReceiptEntryViewModel
{
    public string PurchaseOrderNumber { get; private set; }
    public string ItemId { get; private set; }
    public string Unit { get; private set; }
    public double Quantity { get; private set; }
    public string? Note { get; private set; }

    public CreateFinishedProductReceiptEntryViewModel(string purchaseOrderNumber, string itemId, string unit, double quantity, 
        string? note)
    {
        PurchaseOrderNumber = purchaseOrderNumber;
        ItemId = itemId;
        Unit = unit;
        Quantity = quantity;
        Note = note;
    }
}
