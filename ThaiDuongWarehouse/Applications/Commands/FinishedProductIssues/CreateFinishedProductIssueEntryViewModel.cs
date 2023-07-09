namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductIssues;

public class CreateFinishedProductIssueEntryViewModel
{
    public string PurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }
    public string ItemId { get; private set; }
    public string Unit { get; private set; }
    public string? Note { get; private set; }

    public CreateFinishedProductIssueEntryViewModel(string purchaseOrderNumber, double quantity, string itemId, string unit, 
        string? note)
    {
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
        ItemId = itemId;
        Unit = unit;
        Note = note;
    }
}
