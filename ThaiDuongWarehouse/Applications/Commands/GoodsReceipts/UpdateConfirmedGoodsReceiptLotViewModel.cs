namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class UpdateConfirmedGoodsReceiptLotViewModel
{
    public string GoodsReceiptLotId { get; private set; }
    public string? LocationId { get; set; }
    public double Quantity { get; private set; }
    public string? PurchaseOrderNumber { get; set; }
    public DateTime? ProductionDate { get; set; }
    public DateTime? ExpirationDate { get; set; }

    public UpdateConfirmedGoodsReceiptLotViewModel(string goodsReceiptLotId, string? locationId, double quantity, string? purchaseOrderNumber,
        DateTime? productionDate, DateTime? expirationDate)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        LocationId = locationId;
        Quantity = quantity;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        PurchaseOrderNumber = purchaseOrderNumber;
    }
}
