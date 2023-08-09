namespace ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductIssueAggregate;
public class FinishedProductIssueEntry
{
    public int Id { get; private set; }
    public string PurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }
    public string? Note { get; private set; }

    public int ItemId { get; private set; }
    public int FinishedProductIssueId { get; private set; }
    public Item Item { get; private set; }

    public FinishedProductIssueEntry(string purchaseOrderNumber, double quantity, string? note, int finishedProductIssueId)
    {
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
        Note = note;
        FinishedProductIssueId = finishedProductIssueId;       
    }

    public FinishedProductIssueEntry(string purchaseOrderNumber, double quantity, string? note, int finishedProductIssueId, Item item) : this(purchaseOrderNumber, quantity, note, finishedProductIssueId)
    {
        Item = item;
    }
}
