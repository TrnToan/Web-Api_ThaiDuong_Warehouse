namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Export;

public class GoodsIssuesHistoryViewModel
{
    public string Receiver { get; private set; }
    public string PurchaseOrderNumber { get; private set; }
    public DateTime Timestamp { get; private set; }
    public List<GoodsIssueEntryHistoryViewModel> Entries { get; private set; }
    public GoodsIssuesHistoryViewModel(string receiver, string purchaseOrderNumber, DateTime timestamp, 
        List<GoodsIssueEntryHistoryViewModel> entries)
    {
        Receiver = receiver;
        PurchaseOrderNumber = purchaseOrderNumber;
        Timestamp = timestamp;
        Entries = entries;
    }
}
