namespace ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductIssueAggregate;
public class FinisedProductIssueEntry
{
    public int Id { get; private set; }
    public string PurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }
    public string? Note { get; private set; }

    public int FinishedProductIssueId { get; private set; }
    public Item Item { get; private set; }

    public FinisedProductIssueEntry(int id, string purchaseOrderNumber, double quantity, string? note, int finishedProductIssueId)
    {
        Id = id;
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
        Note = note;
        FinishedProductIssueId = finishedProductIssueId;       
    }

    public FinisedProductIssueEntry(int id, string purchaseOrderNumber, double quantity, string? note, int finishedProductIssueId, Item item) : this(id, purchaseOrderNumber, quantity, note, finishedProductIssueId)
    {
        Item = item;
    }
}
