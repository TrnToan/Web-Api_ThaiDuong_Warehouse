namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;
public class GoodsReceipt : Entity, IAggregateRoot
{
    public string GoodsReceiptId { get; private set; }
    public string? Supplier { get; private set; }
    public DateTime Timestamp { get; private set; }
    public Employee Employee { get; private set; }
    public List<GoodsReceiptLot> Lots { get; private set; }

    private GoodsReceipt() { }

    public GoodsReceipt(string goodsReceiptId, string? supplier, DateTime timestamp, Employee employee) : this() 
    {
        GoodsReceiptId = goodsReceiptId;
        Supplier = supplier;
        Timestamp = timestamp;
        Lots = new List<GoodsReceiptLot>();
        Employee = employee;      
    }

    public void UpdateLot(string oldLotId, string? newLowId, double quantity, List<GoodsReceiptSublot> sublots,
        DateTime? productionDate, DateTime? expirationDate, string? note)
    {
        string lotId = newLowId ?? oldLotId;
        var lot = Lots.FirstOrDefault(gr => gr.GoodsReceiptLotId == oldLotId);
        if (lot == null)
            throw new WarehouseDomainException($"GoodsReceiptLot with Id {lotId} does not exist.");

        lot.Update(lotId, quantity, productionDate, expirationDate, note, sublots);
    }

    public void AddLot(GoodsReceiptLot goodsReceiptLot)
    {
        foreach(var existedLot in Lots)
        {
            if (existedLot.GoodsReceiptLotId == goodsReceiptLot.GoodsReceiptLotId)
            {
                throw new WarehouseDomainException($"GoodsReceiptLot with Id {goodsReceiptLot.GoodsReceiptLotId} already existed in this GoodsReceipt.");
            }
        }
        Lots.Add(goodsReceiptLot);
    }

    public void RemoveLot(GoodsReceiptLot goodsReceiptLot)
    {
        if (goodsReceiptLot == null)
        {
            throw new WarehouseDomainException($"GoodsReceiptLot does not exist.");
        }
        Lots.Remove(goodsReceiptLot);
    }

    public void RemoveItemLotEntities(List<GoodsReceiptLot> lots)
    {
        AddDomainEvent(new RemoveItemLotsDomainEvent(lots));
    }

    public void UpdateItemLotEntity(string oldLotId, string? newLotId, List<ItemLotLocation>? itemLotLocations, double quantity, 
        DateTime? productionDate, DateTime? expirationDate)
    {
        AddDomainEvent(new UpdateItemLotDomainEvent(oldLotId, newLotId, itemLotLocations, quantity, productionDate, expirationDate));
    }

    public void UpdateGoodsReceiptLogEntries(string lotId, int itemId, double changedQuantity, DateTime timestamp)
    {
        double receivedQuantity = changedQuantity;
        double shippedQuantity = 0;
        AddDomainEvent(new UpdateInventoryLogEntriesDomainEvent(lotId, changedQuantity, receivedQuantity, shippedQuantity, itemId, timestamp));
    }

    public void DeletedGoodsReceiptLotLogEntry(string lotId, DateTime timestamp)
    {
        AddDomainEvent(new DeleteInventoryLogEntryDomainEvent(lotId, timestamp));
    }

    public void UpdateLogEntry(string newLotId, string oldLotId, int ItemId, DateTime timestamp)
    {
        AddDomainEvent(new InventoryLogEntryChangedDomainEvent(oldLotId, newLotId, ItemId, timestamp));
    }

    public void Confirm(List<ItemLot> itemLots)
    {
        AddDomainEvent(new ItemLotsImportedDomainEvent(itemLots));
        foreach (var lot in itemLots)
        {
            double receivedQuantity = lot.Quantity;
            AddDomainEvent(new InventoryLogEntryAddedDomainEvent(lot.LotId, lot.Quantity, receivedQuantity, 0, 
                lot.ItemId, lot.Timestamp));
        }
    }
}
