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

    public static void UpdateReceiptEntry(FinishedProductReceiptEntry entry, string purchaseOrderNumber, double quantity)
    {
        entry.UpdateEntry(purchaseOrderNumber, quantity);
    }

    public void RemoveReceiptEntry(Item item, string purchaseOrderNumber)
    {
        var entry = Entries.Find(e => e.Item == item && e.PurchaseOrderNumber == purchaseOrderNumber);
        if (entry == null)
        {
            throw new WarehouseDomainException($"Entry with item {item.ItemName} & {purchaseOrderNumber} not found.");
        }
        Entries.Remove(entry);
    }

    public List<FinishedProductReceiptEntry> GroupAndSumEntries()
    {
        var groupedEntries = Entries
            .GroupBy(entry => new { entry.PurchaseOrderNumber, entry.ItemId })
            .Select(group => new FinishedProductReceiptEntry
            (
                group.Key.PurchaseOrderNumber,
                group.Sum(entry => entry.Quantity),
                group.First().Note,
                Entries[0].FinishedProductReceiptId,
                group.First().Item
            ))
            .ToList();

        Entries = groupedEntries;
        return groupedEntries;
    }

    public void AddFinishedProductInventory(Item item, string purchaseOrderNumber, double quantity, DateTime timestamp)
    {
        AddDomainEvent(new UpdateInventoryOnCreateProductReceiptDomainEvent(purchaseOrderNumber, quantity, timestamp, item));
    }

    public void UpdateFinishedProductInventory(Item item, string oldPurchaseOrderNumber, string newPurchaseOrderNumber,
        double newQuantity, DateTime timestamp)
    {
        var entry = Entries.Find(e => e.Item == item && e.PurchaseOrderNumber == oldPurchaseOrderNumber);
        if (entry == null)
        {
            throw new WarehouseDomainException($"Entry with item {item.ItemName} & {oldPurchaseOrderNumber} not found.");
        }
        double oldQuantity = entry.Quantity;
        // Sửa số PO và số lượng của 1 entry --> Cập nhật tồn kho TP
        AddDomainEvent(new UpdateInventoryOnModifyProductReceiptEntryDomainEvent(oldPurchaseOrderNumber, newPurchaseOrderNumber, 
            oldQuantity, newQuantity, timestamp, item));
    }

    public void RemoveFinishedProductInventory(Item item, string purchaseOrderNumber)
    {
        var entry = Entries.Find(e => e.Item.Id == item.Id && e.PurchaseOrderNumber == purchaseOrderNumber);
        if (entry == null)
        {
            throw new WarehouseDomainException($"Entry with item {item.ItemName} & {purchaseOrderNumber} not found.");
        }

        AddDomainEvent(new UpdateInventoryOnRemoveProductReceiptEntryDomainEvent(item, purchaseOrderNumber, entry.Quantity));
    }
}
