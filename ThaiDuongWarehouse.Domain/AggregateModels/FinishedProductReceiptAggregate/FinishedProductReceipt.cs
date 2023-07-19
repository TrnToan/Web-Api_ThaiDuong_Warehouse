namespace ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductReceiptAggregate;
public class FinishedProductReceipt : Entity, IAggregateRoot
{
    public string FinishedProductReceiptId { get; private set; }
    public DateTime Timestamp { get; private set; }

    public int EmployeeId { get; private set; }
    public Employee Employee { get; private set; }
    public List<FinishedProductReceiptEntry> Entries { get; private set; }

    public FinishedProductReceipt(string finishedProductReceiptId, DateTime timestamp, int employeeId)
    {
        FinishedProductReceiptId = finishedProductReceiptId;
        Timestamp = timestamp;
        EmployeeId = employeeId;
        Entries = new List<FinishedProductReceiptEntry>();
    }

    public void AddReceiptEntry(FinishedProductReceiptEntry entry)
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

    public void UpdateReceiptEntry(FinishedProductReceiptEntry entry, string purchaseOrderNumber, double quantity)
    {
        entry.UpdateEntry(purchaseOrderNumber, quantity);
    }

    public void RemoveReceiptEntry(Item item, string purchaseOrderNumber)
    {
        var entry = Entries.FirstOrDefault(e => e.Item == item && e.PurchaseOrderNumber == purchaseOrderNumber);
        if (entry == null)
        {
            throw new WarehouseDomainException($"Entry with item {item.ItemName} & {purchaseOrderNumber} not found.");
        }
        Entries.Remove(entry);
    }

    public void AddLogEntry(string purchaseOrderNumber, int itemId, double changedQuantity, double receivedQuantity, DateTime timestamp)
    {
        double shippedQuantity = 0;
        AddDomainEvent(new InventoryLogEntryAddedDomainEvent(purchaseOrderNumber, changedQuantity, receivedQuantity, shippedQuantity,
            itemId, timestamp));
    }

    public void UpdateLogEntry(string purchaseOrderNumber, int itemId, double changedQuantity, double receivedQuantity, DateTime timestamp)
    {

    }

    public void AddFinishedProductInventory(Item item, string purchaseOrderNumber, double quantity, DateTime timestamp)
    {
        AddDomainEvent(new UpdateInventoryOnCreateProductReceiptDomainEvent(purchaseOrderNumber, quantity, timestamp, item));
    }

    public void UpdateFinishedProductInventory(Item item, string oldPurchaseOrderNumber, string newPurchaseOrderNumber,
        double quantity, DateTime timestamp)
    {
        AddDomainEvent(new UpdateInventoryOnModifyProductReceiptEntryDomainEvent(oldPurchaseOrderNumber, newPurchaseOrderNumber, 
            quantity, timestamp, item));
    }

    public void RemoveFinishedProductInventory(Item item, string purchaseOrderNumber, DateTime timestamp)
    {
        AddDomainEvent(new RemoveInventoryOnRemoveProductReceiptEntryDomainEvent(item, purchaseOrderNumber, timestamp));
    }
}
