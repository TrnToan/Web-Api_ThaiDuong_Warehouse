namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;
public class GoodsReceiptLot
{
    public string GoodsReceiptLotId { get; private set; }
    public string? LocationId { get; private set; }
    public double Quantity { get; private set; }
    public double? SublotSize { get; private set; }
    public string? PurchaseOrderNumber { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public int ItemId { get; private set; }                      // Foreign Key

    public Item Item { get; private set; }
    public Employee Employee { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private GoodsReceiptLot() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public GoodsReceiptLot(string goodsReceiptLotId, int itemId, Employee employee, 
        string locationId, double quantity, double? sublotSize, string? purchaseOrderNumber, 
        DateTime? productionDate, DateTime? expirationDate) : this()
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        ItemId = itemId;
        Employee = employee;
        LocationId = locationId;
        Quantity = quantity;
        SublotSize = sublotSize;
        PurchaseOrderNumber = purchaseOrderNumber;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
    }

    public GoodsReceiptLot(string goodsReceiptLotId, string? locationId, double quantity, double? sublotSize, 
        string? purchaseOrderNumber, DateTime? productionDate, DateTime? expirationDate, Item item)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        LocationId = locationId;
        Quantity = quantity;
        SublotSize = sublotSize;
        PurchaseOrderNumber = purchaseOrderNumber;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        Item = item;
    }

    public void Update(double quantity, double? sublotSize, string? purchaseOrderNumber, string locationId, 
        DateTime? productionDate, DateTime? expirationDate)
    {
        Quantity = quantity;
        SublotSize = sublotSize;
        PurchaseOrderNumber = purchaseOrderNumber;
        LocationId = locationId;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
    }
}
