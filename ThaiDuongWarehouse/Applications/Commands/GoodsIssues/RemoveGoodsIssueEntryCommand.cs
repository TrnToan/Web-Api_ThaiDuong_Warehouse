namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

public class RemoveGoodsIssueEntryCommand : IRequest<bool>
{
    public string GoodsIssueId { get; private set; }
    public string ItemId { get; private set; }
    public string Unit { get; private set; }

    public RemoveGoodsIssueEntryCommand(string goodsIssueId, string itemId, string unit)
    {
        GoodsIssueId = goodsIssueId;
        ItemId = itemId;
        Unit = unit;
    }
}
