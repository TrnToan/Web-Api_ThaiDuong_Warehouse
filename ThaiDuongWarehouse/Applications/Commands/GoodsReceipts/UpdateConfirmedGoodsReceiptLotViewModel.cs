namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class UpdateConfirmedGoodsReceiptLotViewModel
{
    public string GoodsReceiptLotId { get; private set; }
    public double Quantity { get; private set; }

    public UpdateConfirmedGoodsReceiptLotViewModel(string goodsReceiptLotId, double quantity)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        Quantity = quantity;
    }
}
