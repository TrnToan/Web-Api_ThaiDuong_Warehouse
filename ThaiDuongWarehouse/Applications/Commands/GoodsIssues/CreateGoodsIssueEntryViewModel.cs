namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

public class CreateGoodsIssueEntryViewModel
{
    public string ItemId { get; private set; }
    public string Unit { get; private set; }
    public double RequestedQuantity { get; private set; }
    public CreateGoodsIssueEntryViewModel(string itemId, string unit, double requestedQuantity)
    {
        ItemId = itemId;
        Unit = unit;
        RequestedQuantity = requestedQuantity;
    }
}
