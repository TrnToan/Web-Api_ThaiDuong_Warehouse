namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;
public class GoodsIssue : Entity, IAggregateRoot
{
    public string GoodsIssueId { get; private set; }
    public string? Receiver { get; private set; }
    public DateTime Timestamp { get; private set; }

    // ForeignKey
    public int EmployeeId { get; private set; }
    public Employee Employee { get; private set; }
    public List<GoodsIssueEntry> Entries { get; private set; }

    public GoodsIssue(string goodsIssueId, DateTime timestamp, string? receiver, int employeeId)
    {
        GoodsIssueId = goodsIssueId;
        Timestamp = timestamp;
        Receiver = receiver;
        Entries = new List<GoodsIssueEntry>();
        EmployeeId = employeeId;
    }
    public void AddEntry(Item item, double requestedQuantity)
    {
        var entry = new GoodsIssueEntry(item, requestedQuantity);
        foreach(var existedEntry in Entries)
        {
            if(entry.Item == existedEntry.Item)
            {
                throw new WarehouseDomainException($"Entry with Item {entry.Item} has already existed in the GoodsIssue");
            }
        }
        Entries.Add(entry);
    }
    public void UpdateEntry(string itemId, string unit, double quantity)
    {
        var entry = Entries.SingleOrDefault(entry => entry.Item.ItemId == itemId && entry.Item.Unit == unit);
        if (entry == null)
        {
            throw new WarehouseDomainException($"Entry having item {itemId} with unit {unit} doesn't exist in the current GoodsIssue.");
        }
        entry.UpdateEntry(quantity);
    }
    public void Addlot(string itemId, GoodsIssueLot lot) 
    {
        var entry = Entries.FirstOrDefault(e => e.Item.ItemId == itemId);
        if (entry == null)
        {
            throw new WarehouseDomainException("Entry doesn't exist");
        }
        entry.AddLot(lot);
    }
    public void Confirm(List<ItemLot> lots)
    {
        foreach (GoodsIssueEntry entry in Entries)
        {
            foreach (GoodsIssueLot lot in entry.Lots)
            {
                this.AddDomainEvent(new InventoryLogEntryChangedDomainEvent(lot.GoodsIssueLotId, -lot.Quantity, entry.Item.Id));
            }
        }

        var completelyExportedLots = new List<ItemLot>();

        foreach (var lot in lots)
        {
            var goodsIssueLot = Entries.SelectMany(e => e.Lots).First(l => l.GoodsIssueLotId == lot.LotId);           

            if (goodsIssueLot.Quantity == lot.Quantity)
            {
                completelyExportedLots.Add(lot);
            }
            else
            {
                this.AddDomainEvent(new ItemLotInformationChangedDomainEvent(lot.LotId, goodsIssueLot.Quantity));   
            }
        }
        this.AddDomainEvent(new ItemLotsExportedDomainEvent(completelyExportedLots));      
    }
}