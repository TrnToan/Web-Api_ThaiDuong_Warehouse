using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class CreateGoodsReceiptLotViewModel
{
    public string GoodsReceiptLotId { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
    public string ItemId { get; set; }
    public string PurchaseOrderNumber { get; set; }
    public string EmployeeId { get; set; }
    public string? Note { get; set; }
    public CreateGoodsReceiptLotViewModel(string goodsReceiptLotId, double quantity, string unit,
        string itemId, string purchaseOrderNumber, string employeeId, string? note)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        Quantity = quantity;
        Unit = unit;
        ItemId = itemId;
        PurchaseOrderNumber = purchaseOrderNumber;
        EmployeeId = employeeId;
        Note = note;
    }
}
