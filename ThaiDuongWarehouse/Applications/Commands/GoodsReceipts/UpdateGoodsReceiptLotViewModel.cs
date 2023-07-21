namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class UpdateGoodsReceiptLotViewModel
{
    public string OldGoodsReceiptLotId { get; private set; }
    public string? NewGoodsReceiptLotId { get; private set; }
    public List<ItemLotLocationViewModel>? ItemLotLocations { get; private set; }
    public double Quantity { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string? Note { get; private set; }

    public UpdateGoodsReceiptLotViewModel(string oldGoodsReceiptLotId, string? newGoodsReceiptLotId, 
        List<ItemLotLocationViewModel>? itemLotLocations, double quantity, DateTime? productionDate, DateTime? expirationDate, string? note)
    {
        OldGoodsReceiptLotId = oldGoodsReceiptLotId;
        NewGoodsReceiptLotId = newGoodsReceiptLotId;
        ItemLotLocations = itemLotLocations;
        Quantity = quantity;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        Note = note;
    }
}
