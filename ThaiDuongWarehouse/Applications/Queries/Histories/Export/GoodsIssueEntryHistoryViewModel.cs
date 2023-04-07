namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Export;

public class GoodsIssueEntryHistoryViewModel
{
    public ItemViewModel Item { get; private set; }
    public string Unit { get; private set; }
    public List<GoodsIssueLotsHistoryViewModel> Lots { get; private set; }
    public GoodsIssueEntryHistoryViewModel(ItemViewModel item, string unit, List<GoodsIssueLotsHistoryViewModel> lots)
    {
        Item = item;
        Unit = unit;
        Lots = lots;
    }
} 
