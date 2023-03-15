namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;
public class GoodsReceipt : Entity, IAggregateRoot
{
    public string GoodsReceiptId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public bool IsConfirmed { get; private set; }
    public Employee Employee { get; private set; }
    public List<GoodsReceiptLot> Lots { get; private set; }

    private GoodsReceipt() { }

    public GoodsReceipt(string goodsReceiptId, DateTime timestamp, bool isConfirmed, Employee employee)
    {
        GoodsReceiptId = goodsReceiptId;
        Timestamp = timestamp;
        IsConfirmed = isConfirmed;
        Lots = new List<GoodsReceiptLot>();
        Employee = employee;
    }

    public void Update(string lotId, double quantity, double sublotSize, string? purchaseOrderNumber,
        string locationId, DateTime productionDate, DateTime expirationDate)
    {
        var lot = Lots.FirstOrDefault(e => e.GoodsReceiptLotId == lotId);
        if (lot == null)
        {
            throw new WarehouseDomainException("Lot doesn't exist in the current GoodsReceipt");
        }
        lot.Update(quantity, sublotSize, purchaseOrderNumber, locationId, productionDate, expirationDate);
    }
    public void AddLot(GoodsReceiptLot goodsReceiptLot)
    {
        if(goodsReceiptLot is null)
            throw new ArgumentNullException(nameof(goodsReceiptLot));

        Lots.Add(goodsReceiptLot);
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
