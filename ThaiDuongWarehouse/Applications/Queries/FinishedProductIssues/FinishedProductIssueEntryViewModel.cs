namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductIssues;

public class FinishedProductIssueEntryViewModel
{
    public string PurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }
    public string? Note { get; private set; }
    public ItemViewModel Item { get; private set; }

    public FinishedProductIssueEntryViewModel(string purchaseOrderNumber, double quantity, string? note, ItemViewModel item)
    {
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
        Note = note;
        Item = item;
    }
}
