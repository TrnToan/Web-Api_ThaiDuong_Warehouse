namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Export;

public class GoodsIssueEntryHistoryViewModel
{
    public ItemViewModel Item { get; private set; }
    public List<GoodsIssueLotHistoryViewModel> Lots { get; private set; }
    public GoodsIssueEntryHistoryViewModel(ItemViewModel item, List<GoodsIssueLotHistoryViewModel> lots)
    {
        Item = item;
        Lots = lots;
    }
} 
