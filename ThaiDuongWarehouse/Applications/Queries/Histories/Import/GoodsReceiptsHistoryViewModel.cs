namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Import;

public class GoodsReceiptsHistoryViewModel
{
    public string Supplier { get; private set; }
    public DateTime Timestamp { get; private set; }
    public List<GoodsReceiptLotsHistoryViewModel> Lots { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public GoodsReceiptsHistoryViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {    }
    public GoodsReceiptsHistoryViewModel(string supplier, DateTime timestamp,
        List<GoodsReceiptLotsHistoryViewModel> lots)
    {
        Supplier = supplier;
        Timestamp = timestamp;
        Lots = lots;
    }
}
