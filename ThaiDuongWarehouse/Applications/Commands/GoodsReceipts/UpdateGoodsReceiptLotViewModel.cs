namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class UpdateGoodsReceiptLotViewModel
{
    public string GoodsReceiptLotId { get; private set; }
    public string LocationId { get; private set; }
    public double Quantity { get; private set; }
    public double SublotSize { get; private set; }
    public string SublotUnit { get; private set; }
    public string PurchaseOrderNumber { get; private set; }
    public DateTime ProductionDate { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public string? Note { get; private set; }

    public UpdateGoodsReceiptLotViewModel(string goodsReceiptLotId, string locationId, double quantity, double sublotSize, 
        string sublotUnit, string purchaseOrderNumber, DateTime productionDate, DateTime expirationDate, string? note)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        LocationId = locationId;
        Quantity = quantity;
        SublotSize = sublotSize;
        SublotUnit = sublotUnit;
        PurchaseOrderNumber = purchaseOrderNumber;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        Note = note;
    }
}
