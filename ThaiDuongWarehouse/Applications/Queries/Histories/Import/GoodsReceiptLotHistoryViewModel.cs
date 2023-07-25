namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Import;

public class GoodsReceiptLotHistoryViewModel
{
    public string GoodsReceiptLotId { get; private set; }
    public double Quantity { get; private set; }
    public ItemViewModel Item { get; private set; }
    public List<GoodsReceiptSublotViewModel> Sublots { get; private set; }
    public string? Note { get; private set; }

    public GoodsReceiptLotHistoryViewModel(string goodsReceiptLotId, double quantity, ItemViewModel item, string? note, 
        List<GoodsReceiptSublotViewModel> sublots)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        Quantity = quantity;
        Item = item;
        Note = note;
        Sublots = sublots;
    }
}
