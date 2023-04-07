namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsReceipt;

public class GoodsReceiptLotViewModel
{
    public string GoodsReceiptLotId { get; private set; }
    public string? LocationId { get; private set; }
    public double Quantity { get; private set; }
    public double? SublotSize { get; private set; }
    public string? PurchaseOrderNumber { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public ItemViewModel Item { get; private set; }
    public EmployeeViewModel Employee { get; private set; }
    public string? Note { get; private set; }

    public GoodsReceiptLotViewModel(string goodsReceiptLotId, string? locationId, double quantity, 
        double? sublotSize, string? purchaseOrderNumber, DateTime? productionDate, DateTime? expirationDate, 
        ItemViewModel item, EmployeeViewModel employee, string? note)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        LocationId = locationId;
        Quantity = quantity;
        SublotSize = sublotSize;
        PurchaseOrderNumber = purchaseOrderNumber;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        Item = item;
        Employee = employee;
        Note = note;
    }
}
