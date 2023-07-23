namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

public class UpdateFinishedProductReceiptEntryViewModel
{
    public string ItemId { get; private set; }
    public string OldPurchaseOrderNumber { get; private set; }
    public string? NewPurchaseOrderNumber { get; private set; }
    public string Unit { get; private set; }
    public double Quantity { get; private set; }

    public UpdateFinishedProductReceiptEntryViewModel(string itemId, string oldPurchaseOrderNumber, string? newPurchaseOrderNumber,
        string unit, double quantity)
    {
        ItemId = itemId;
        OldPurchaseOrderNumber = oldPurchaseOrderNumber;
        NewPurchaseOrderNumber = newPurchaseOrderNumber;
        Unit = unit;
        Quantity = quantity;
    }
}
