namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class UpdateGoodsReceiptLotViewModel
{
    public string OldGoodsReceiptLotId { get; private set; }
    public string NewGoodsReceiptLotId { get; private set; }
    public string? LocationId { get; private set; }
    public double Quantity { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string? Note { get; private set; }

    public UpdateGoodsReceiptLotViewModel(string oldGoodsReceiptLotId, string newGoodsReceiptLotId, string? locationId, 
        double quantity, DateTime? productionDate, DateTime? expirationDate, string? note)
    {
        OldGoodsReceiptLotId = oldGoodsReceiptLotId;
        NewGoodsReceiptLotId = newGoodsReceiptLotId;
        LocationId = locationId;
        Quantity = quantity;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        Note = note;
    }
}
