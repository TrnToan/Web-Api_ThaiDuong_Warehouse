using ThaiDuongWarehouse.Domain.DomainEvents.FinishedProductIssueEvents;

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

    public void RemoveIssueEntry(string itemId, string purchaseOrderNumber)
    {
        var existedEntry = Entries.Find(entry => entry.Item.ItemId == itemId && entry.PurchaseOrderNumber == purchaseOrderNumber);
        if (existedEntry is null)
        {
            throw new WarehouseDomainException($"ProductIssueEntry with Item {itemId} and {purchaseOrderNumber} not found.");
        }
        Entries.Remove(existedEntry);
    }

    public void UpdateFinishedProductInventory(Item item, string purchaseOrderNumber, double quantity)
    {
        AddDomainEvent(new UpdateInventoryOnCreateProductIssueDomainEvent(item, purchaseOrderNumber, quantity));
    }

    public void RestoreProductInventory(Item item, string purchaseOrderNumber)
    {
        var existedEntry = Entries.Find(entry => entry.Item.ItemId == item.ItemId && entry.PurchaseOrderNumber == purchaseOrderNumber);
        if (existedEntry is null)
        {
            throw new WarehouseDomainException($"ProductIssueEntry with Item {item.ItemId} and {purchaseOrderNumber} not found.");
        }

        AddDomainEvent(new UpdateInventoryOnRemoveProductIssueEntryDomainEvent(item, purchaseOrderNumber, existedEntry.Quantity));
    }
}
