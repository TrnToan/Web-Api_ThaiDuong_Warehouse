namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsReceipt;

public class GoodsReceiptLotViewModel
{
    public string GoodsReceiptLotId { get; private set; }
    public double Quantity { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public ItemViewModel Item { get; private set; }
    public EmployeeViewModel Employee { get; private set; }
    public List<GoodsReceiptSublotViewModel> GoodsReceiptSublots { get; private set; }
    public string? Note { get; private set; }
    public bool IsExported { get; set; } = false;

    public GoodsReceiptLotViewModel(string goodsReceiptLotId, double quantity, DateTime? productionDate, 
        DateTime? expirationDate, ItemViewModel item, EmployeeViewModel employee, List<GoodsReceiptSublotViewModel> sublots, string? note)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        Quantity = quantity;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        Item = item;
        Employee = employee;
        GoodsReceiptSublots = sublots;
        Note = note;
    }
}
