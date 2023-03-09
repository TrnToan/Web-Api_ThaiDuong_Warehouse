namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;
public class GoodsReceiptLot
{
    public string GoodsReceiptLotId { get; private set; }
    public int ItemId { get; private set; }                      // Foreign Key
    public int EmployeeId { get; private set; }                  // Foreign Key
    public string LocationId { get; private set; }
    public double Quantity { get; private set; }
    public double? SublotSize { get; private set; }
    public string? PurchaseOrderNumber { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public int GoodsReceiptId { get; private set; }              // Foreign Key

    public Item Item { get; private set; }
    public Employee Employee { get; private set; }

    public GoodsReceiptLot(string goodsReceiptLotId, int itemId, int employeeId, 
        string locationId, double quantity, double? sublotSize, string? purchaseOrderNumber, 
        DateTime? productionDate, DateTime? expirationDate, int goodsReceiptId)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        ItemId = itemId;
        EmployeeId = employeeId;
        LocationId = locationId;
        Quantity = quantity;
        SublotSize = sublotSize;
        PurchaseOrderNumber = purchaseOrderNumber;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        GoodsReceiptId = goodsReceiptId;
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
