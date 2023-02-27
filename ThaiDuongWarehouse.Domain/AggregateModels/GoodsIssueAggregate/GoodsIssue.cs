namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;
public class GoodsIssue : Entity, IAggregateRoot
{
    public string GoodsIssueId { get; private set; }
    public string Receiver { get; private set; }
    public string? PurchaseOrderNumber { get; private set; }
    public bool IsConfirmed { get; private set; }
    public DateTime Timestamp { get; private set; }
    public List<GoodsIssueEntry> Entries { get; private set; }

    public GoodsIssue(string goodsIssueId, string? purchaseOrderNumber, DateTime timestamp, 
        string receiver)
    {
        GoodsIssueId = goodsIssueId;
        PurchaseOrderNumber = purchaseOrderNumber;
        Timestamp = timestamp;
        Receiver = receiver;
        Entries = new List<GoodsIssueEntry>();
    }
    void AddEntry(Item item, double? requestedSublotSize, double requestedQuantity)
    {
        var entry = new GoodsIssueEntry(item, requestedSublotSize, requestedQuantity);
        foreach(var existedEntry in Entries)
        {
            if(entry.Item == existedEntry.Item)
            {
                throw new WarehouseDomainException($"Entry with Item {entry.Item} has already existed in the GoodsIssue");
            }
        }
        Entries.Add(entry);
    }
    public void SetQuantity(string itemId, double quantity)
    {
        var entry = Entries.FirstOrDefault(e => e.Item.ItemId == itemId);
        if (entry == null) 
        {
            throw new ArgumentException("Entry doesn't exist");
        }
        entry.SetQuantity(quantity);
    }
    public void Addlot(string itemId, GoodsIssueLot goodsIssueLot) 
    {
        var entry = Entries.FirstOrDefault(e => e.Item.ItemId == itemId);
        if (entry == null)
        {
            throw new ArgumentException("Entry doesn't exist");
        }
        entry.AddLot(goodsIssueLot);
    }
    public void Confirm()
    {
        this.AddDomainEvent(new GoodsIssueConfirmedDomainEvent(IsConfirmed));
    }
}
