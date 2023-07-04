namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsReceipt;

public class GoodsReceiptViewModel
{
    public string GoodsReceiptId { get; private set; }
    public string Supplier { get; private set; }
    public DateTime Timestamp { get; private set; }
    public EmployeeViewModel Employee { get; private set; }
    public List<GoodsReceiptLotViewModel> Lots { get; private set; }

    public GoodsReceiptViewModel(string goodsReceiptId, DateTime timestamp, 
        EmployeeViewModel employee, List<GoodsReceiptLotViewModel> lots, string supplier)
    {
        GoodsReceiptId = goodsReceiptId;
        Timestamp = timestamp;
        Employee = employee;
        Lots = lots;
        Supplier = supplier;
    }
}
