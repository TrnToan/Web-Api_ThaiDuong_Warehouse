namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsIssues;

public class GoodsIssueEntryViewModel
{
    public double? RequestedSublotSize { get; private set; }
    public double RequestedQuantity { get; private set; }
    public ItemViewModel Item { get; private set; }
    public List<GoodsIssueLotViewModel> GoodsIssueLots { get; private set; }
    public GoodsIssueEntryViewModel(double? requestedSublotSize, double requestedQuantity, 
        ItemViewModel item, List<GoodsIssueLotViewModel> goodsIssueLots)
    {
        RequestedSublotSize = requestedSublotSize;
        RequestedQuantity = requestedQuantity;
        Item = item;
        GoodsIssueLots = goodsIssueLots;
    }
}

