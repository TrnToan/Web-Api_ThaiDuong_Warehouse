using ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate;

namespace ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductReceiptAggregate;
public class FinishedProductReceiptEntry
{
    public int Id { get; private set; }
    public string PurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }
    public string? Note { get; private set; }

    public int FinishedProductReceiptId { get; private set; }
    public Item Item { get; private set; }

    public FinishedProductReceiptEntry(string purchaseOrderNumber, double quantity, string? note, int finishedProductReceiptId)
    {
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
        Note = note;
        FinishedProductReceiptId = finishedProductReceiptId;
    }

    public FinishedProductReceiptEntry(string purchaseOrderNumber, double quantity, string? note, int finishedProductReceiptId, Item item) : this(purchaseOrderNumber, quantity, note, finishedProductReceiptId)
    {
        Item = item;
    }
}
