namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Import;

public class GoodsReceiptLotsHistoryViewModel
{
    public string GoodsReceiptLotId { get; private set; }
    public double Quantity { get; private set; }
    public string? PurchaseOrderNumber { get; private set; }
    public ItemViewModel Item { get; private set; }
    public string? Note { get; private set; }
    public GoodsReceiptLotsHistoryViewModel(string goodsReceiptLotId, double quantity, string? purchaseOrderNumber,
        ItemViewModel item, string? note)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        Quantity = quantity;
        PurchaseOrderNumber = purchaseOrderNumber;
        Item = item;
        Note = note;
    }
}
