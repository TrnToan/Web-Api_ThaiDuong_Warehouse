namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;
public class GoodsReceiptLot
{
    public string GoodsReceiptLotId { get; private set; }
    public string? LocationId { get; private set; }
    public double Quantity { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string? Note { get; private set; }
    // Foreign Key
    public int ItemId { get; private set; }                      
    public int GoodsReceiptId { get; private set; }

    public Item Item { get; private set; }
    public Employee Employee { get; private set; }

    private GoodsReceiptLot() { }
    public GoodsReceiptLot(string goodsReceiptLotId, string? locationId, double quantity, 
        DateTime? productionDate, DateTime? expirationDate, int itemId, int goodsReceiptId)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        LocationId = locationId;
        Quantity = quantity;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        ItemId = itemId;
        GoodsReceiptId = goodsReceiptId;
    }
    public GoodsReceiptLot(string goodsReceiptLotId, double quantity, Employee employee, Item item, string? note, int goodsReceiptId)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        Quantity = quantity;
        Employee = employee;
        Item = item;
        Note = note;
        GoodsReceiptId = goodsReceiptId;
    }

    public void Update(double quantity, string locationId, DateTime? productionDate, DateTime? expirationDate, string? note)
    {
        Quantity = quantity;
        LocationId = locationId;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        Note = note;
    }

    public void UpdateConfirmedLot(double quantity, string? purchaseOrderNumber, string? locationId,
        DateTime? productionDate, DateTime? expirationDate)
    {
        Quantity = quantity;
        LocationId = locationId;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
    }

    public void SetQuantity(double quantity)
    {
        Quantity = quantity;
    }
}
