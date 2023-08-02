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
        var newEntry = new GoodsIssueEntry(item, requestedQuantity);
        foreach(var existedEntry in Entries)
        {
            if(newEntry.Item == existedEntry.Item)
            {
                throw new WarehouseDomainException($"Entry with Item {newEntry.Item.ItemId} has already existed in the GoodsIssue");
            }
        }
        Entries.Add(newEntry);
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
    public void Addlot(string itemId, string unit, GoodsIssueLot lot) 
    {
        var entry = Entries.Find(e => e.Item.ItemId == itemId && e.Item.Unit == unit);
        if (entry == null)
        {
            throw new WarehouseDomainException($"Entry with Item {itemId} doesn't exist");
        }
        entry.AddLot(lot);
    }

    public void Confirm(List<ItemLot> itemLots)
    {
        List<ItemLot> removedLots = new();
        foreach (var itemLot in itemLots)
        {
            var goodsIssueLot = Entries.SelectMany(e => e.Lots).First(l => l.GoodsIssueLotId == itemLot.LotId);

            if (goodsIssueLot.Quantity > itemLot.Quantity)
            {
                throw new WarehouseDomainException($"Invalid goodsIssueLot quantity, {goodsIssueLot.Quantity}");
            }
            else if (goodsIssueLot.Quantity == itemLot.Quantity)
            {
                removedLots.Add(itemLot);
            }
            else
            {
                AddDomainEvent(new ItemLotInformationChangedDomainEvent(itemLot, goodsIssueLot));
            }
        }

        if (removedLots.Count > 0)
        {
            AddDomainEvent(new ItemLotExportedDomainEvent(removedLots));
        }     

        foreach (GoodsIssueEntry entry in Entries)
        {
            foreach (GoodsIssueLot lot in entry.Lots)
            {
                var newLot = itemLots.Find(l => l.LotId == lot.GoodsIssueLotId);
                if (newLot is not null)
                {
                    AddDomainEvent(new InventoryLogEntryAddedDomainEvent(lot.GoodsIssueLotId, -lot.Quantity, 0, lot.Quantity,
                    entry.Item.Id, Timestamp));
                }               
            }
        }       
    }
}