using ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductReceiptAggregate;

namespace ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductIssueAggregate;
public class FinishedProductIssue : Entity, IAggregateRoot
{
    public string FinishedProductIssueId { get; private set; }
    public string? Receiver { get; private set; }
    public DateTime Timestamp { get; private set; }

    public int EmployeeId { get; private set; }
    public Employee Employee { get; private set; }
    public List<FinishedProductIssueEntry> Entries { get; private set; }

    public FinishedProductIssue(string finishedProductIssueId, string? receiver, DateTime timestamp, int employeeId)
    {
        FinishedProductIssueId = finishedProductIssueId;
        Receiver = receiver;
        Timestamp = timestamp;
        EmployeeId = employeeId;
        Entries = new List<FinishedProductIssueEntry>();
    }

    public void AddIssueEntry(FinishedProductIssueEntry entry)
    {
        foreach (var existedEntry in Entries)
        {
            if (entry.Item == existedEntry.Item && entry.PurchaseOrderNumber == existedEntry.PurchaseOrderNumber)
            {
                throw new WarehouseDomainException($"One of the Entries already existed in this Receipt.");
            }
        }
        Entries.Add(entry);
    }

    public void UpdateFinishedProductInventory(Item item, string purchaseOrderNumber, double quantity, DateTime timestamp)
    {
        AddDomainEvent(new UpdateProductInventoryOnProductReceiptDomainEvent(purchaseOrderNumber, quantity, timestamp, item));
    }
}
