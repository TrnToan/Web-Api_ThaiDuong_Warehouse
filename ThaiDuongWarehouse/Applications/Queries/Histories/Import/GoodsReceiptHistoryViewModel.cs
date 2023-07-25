namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Import;

public class GoodsReceiptHistoryViewModel
{
    public string Supplier { get; private set; }
    public DateTime Timestamp { get; private set; }
    public List<GoodsReceiptLotHistoryViewModel> Lots { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public GoodsReceiptHistoryViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {    }
    public GoodsReceiptHistoryViewModel(string supplier, DateTime timestamp,
        List<GoodsReceiptLotHistoryViewModel> lots)
    {
        Supplier = supplier;
        Timestamp = timestamp;
        Lots = lots;
    }
}
