namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Export;

public class GoodsIssueHistoryViewModel
{
    public string Receiver { get; private set; }
    public DateTime Timestamp { get; private set; }
    public List<GoodsIssueEntryHistoryViewModel> Entries { get; private set; }
    public GoodsIssueHistoryViewModel(string receiver, DateTime timestamp, List<GoodsIssueEntryHistoryViewModel> entries)
    {
        Receiver = receiver;
        Timestamp = timestamp;
        Entries = entries;
    }
}
