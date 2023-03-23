using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;
public class GoodsReceipt : Entity, IAggregateRoot
{
    public string GoodsReceiptId { get; private set; }
    public string? Supplier { get; private set; }
    public DateTime Timestamp { get; private set; }
    public bool IsConfirmed { get; private set; } = false;
    public Employee Employee { get; private set; }
    public List<GoodsReceiptLot> Lots { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private GoodsReceipt() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public GoodsReceipt(string goodsReceiptId, DateTime timestamp, bool isConfirmed, 
        Employee employee, string? supplier)
    {
        GoodsReceiptId = goodsReceiptId;
        Timestamp = timestamp;
        IsConfirmed = isConfirmed;
        Lots = new List<GoodsReceiptLot>();
        Employee = employee;
        Supplier = supplier;
    }

    public void UpdateLot(string lotId, double quantity, double sublotSize, string? purchaseOrderNumber,
        string locationId, DateTime productionDate, DateTime expirationDate)
    {
        var lot = Lots.FirstOrDefault(e => e.GoodsReceiptLotId == lotId);
        if (lot == null)
        {
            throw new WarehouseDomainException("Lot doesn't exist in the current GoodsReceipt");
        }
        lot.Update(quantity, sublotSize, purchaseOrderNumber, locationId, productionDate, expirationDate);
    }
    public void AddLot(ItemLot itemLot)
    {
        if (itemLot is null)
            throw new ArgumentNullException(nameof(itemLot));

        var goodsReceiptLot = new GoodsReceiptLot(itemLot.LotId, itemLot.Location?.LocationId, itemLot.Quantity, itemLot.SublotSize,
            itemLot.PurchaseOrderNumber, itemLot.ProductionDate, itemLot.ExpirationDate, itemLot.Item);
        Lots.Add(goodsReceiptLot);
    }
    public void AddLots(IEnumerable<ItemLot> itemLots)
    {
        foreach(var lot in itemLots)
        {
            AddLot(lot);    
        }
        this.AddDomainEvent(new ItemLotsImportedDomainEvent(itemLots));
    }
    public void RemoveLot(string goodsReceiptLotId)
    {
        var lot = Lots.FirstOrDefault(e => e.GoodsReceiptLotId == goodsReceiptLotId);
        if (lot == null)
        {
            throw new WarehouseDomainException("Lot doesn't exist in the current GoodsReceipt");
        }
        Lots.Remove(lot);
    }
    public void Confirm(DateTime timestamp)
    {
        IsConfirmed = true;
        Timestamp = timestamp;
    }
}
