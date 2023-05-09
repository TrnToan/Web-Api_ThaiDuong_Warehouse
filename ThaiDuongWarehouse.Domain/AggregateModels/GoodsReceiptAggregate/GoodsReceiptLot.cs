namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;
public class GoodsReceiptLot
{
    public string GoodsReceiptLotId { get; private set; }
    public string? LocationId { get; private set; }
    public double Quantity { get; private set; }
    public string Unit { get; private set; }
    public double? SublotSize { get; private set; }
    public string? SublotUnit { get; private set; }
    public string? PurchaseOrderNumber { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string? Note { get; private set; }
    // Foreign Key
    public int ItemId { get; private set; }                      
    public int GoodsReceiptId { get; private set; }

    public Item Item { get; private set; }
    public Employee Employee { get; private set; }

    private GoodsReceiptLot() { }
    public GoodsReceiptLot(string goodsReceiptLotId, string? locationId, double quantity, string unit, double? sublotSize, 
        string? purchaseOrderNumber, DateTime? productionDate, DateTime? expirationDate, int itemId, int goodsReceiptId, string? sublotUnit)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        LocationId = locationId;
        Quantity = quantity;
        Unit = unit;
        SublotSize = sublotSize;
        PurchaseOrderNumber = purchaseOrderNumber;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        ItemId = itemId;
        GoodsReceiptId = goodsReceiptId;
        SublotUnit = sublotUnit;
    }
    public GoodsReceiptLot(string goodsReceiptLotId, double quantity, string unit, double? sublotSize, string? sublotUnit, 
        string purchaseOrderNumber, Employee employee, Item item, string? note, int goodsReceiptId)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        Quantity = quantity;
        Unit = unit;
        SublotSize = sublotSize;
        SublotUnit = sublotUnit;
        PurchaseOrderNumber = purchaseOrderNumber;
        Employee = employee;
        Item = item;
        Note = note;
        GoodsReceiptId = goodsReceiptId;
    }

    public void Update(double quantity, double? sublotSize, string? sublotUnit, string? purchaseOrderNumber, string locationId, 
        DateTime? productionDate, DateTime? expirationDate, string? note)
    {
        Quantity = quantity;
        SublotSize = sublotSize;
        SublotUnit = sublotUnit;
        PurchaseOrderNumber = purchaseOrderNumber;
        LocationId = locationId;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        Note = note;
    }

    public void SetQuantity(double quantity)
    {
        Quantity = quantity;
    }
}
