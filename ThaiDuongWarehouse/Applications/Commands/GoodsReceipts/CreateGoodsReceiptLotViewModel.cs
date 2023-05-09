namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class CreateGoodsReceiptLotViewModel
{
    public string GoodsReceiptLotId { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
    public double? SublotSize { get; set; }
    public string SublotUnit { get; set; }
    public string ItemId { get; set; }
    public string PurchaseOrderNumber { get; set; }
    public string EmployeeId { get; set; }
    public string? Note { get; set; }
    public CreateGoodsReceiptLotViewModel(string goodsReceiptLotId, double quantity, string unit, double? sublotSize, string sublotUnit,
        string itemId, string purchaseOrderNumber, string employeeId, string? note)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        Quantity = quantity;
        Unit = unit;
        SublotSize = sublotSize;
        SublotUnit = sublotUnit;
        ItemId = itemId;
        PurchaseOrderNumber = purchaseOrderNumber;
        EmployeeId = employeeId;
        Note = note;
    }
}
