namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class CreateGoodsReceiptLotViewModel
{
    public string GoodsReceiptLotId { get; set; }
    public double Quantity { get; set; }
    public string ItemId { get; set; }
    public string Unit { get; set; }
    public string EmployeeId { get; set; }
    public string? Note { get; set; }
    public CreateGoodsReceiptLotViewModel(string goodsReceiptLotId, double quantity, string unit,
        string itemId, string employeeId, string? note)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        Quantity = quantity;
        Unit = unit;
        ItemId = itemId;
        EmployeeId = employeeId;
        Note = note;
    }
}
 