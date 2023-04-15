﻿namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;
public class GoodsIssue : Entity, IAggregateRoot
{
    public string GoodsIssueId { get; private set; }
    public string Receiver { get; private set; }
    public string? PurchaseOrderNumber { get; private set; }
    public bool IsConfirmed { get; private set; } = false;
    public DateTime Timestamp { get; private set; }
    // ForeignKey
    public int EmployeeId { get; private set; }
    public Employee Employee { get; private set; }
    public List<GoodsIssueEntry> Entries { get; private set; }

    public GoodsIssue(string goodsIssueId, string? purchaseOrderNumber, DateTime timestamp,
        string receiver, int employeeId)
    {
        GoodsIssueId = goodsIssueId;
        PurchaseOrderNumber = purchaseOrderNumber;
        Timestamp = timestamp;
        Receiver = receiver;
        Entries = new List<GoodsIssueEntry>();
        EmployeeId = employeeId;
    }
    public void AddEntry(Item item, string unit, double? requestedSublotSize, double requestedQuantity)
    {
        var entry = new GoodsIssueEntry(item, unit, requestedSublotSize, requestedQuantity);
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
    public void Addlot(string itemId, GoodsIssueLot lot) 
    {
        var entry = Entries.FirstOrDefault(e => e.Item.ItemId == itemId);
        if (entry == null)
        {
            throw new ArgumentException("Entry doesn't exist");
        }
        entry.AddLot(lot);
    }
    public void Confirm(DateTime timestamp, List<ItemLot> lots)
    {
        foreach (GoodsIssueEntry entry in Entries)
        {
            foreach (GoodsIssueLot lot in entry.Lots)
            {
                this.AddDomainEvent(new InventoryLogEntryChangedDomainEvent(lot.GoodsIssueLotId, -lot.Quantity, entry.Item.Id, timestamp));
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
        
        IsConfirmed = true;
    }
}