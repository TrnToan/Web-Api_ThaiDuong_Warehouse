namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Export;

public class GoodsIssueLotsHistoryViewModel
{
    public string GoodsIssueLotId { get; private set; }
    public double Quantity { get; private set; }
    public string? Note { get; private set; }
    public GoodsIssueLotsHistoryViewModel(string goodsIssueLotId, double quantity, string? note)
    {
        GoodsIssueLotId = goodsIssueLotId;
        Quantity = quantity;
        Note = note;
    }
}
