namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

public class RemoveFinishedProductEntryViewModel
{
    public string ItemId { get; set; }
    public string Unit { get; set; }
    public string PurchaseOrderNumber { get; set; }

    public RemoveFinishedProductEntryViewModel(string itemId, string unit, string purchaseOrderNumber)
    {
        ItemId = itemId;
        Unit = unit;
        PurchaseOrderNumber = purchaseOrderNumber;
    }
}
