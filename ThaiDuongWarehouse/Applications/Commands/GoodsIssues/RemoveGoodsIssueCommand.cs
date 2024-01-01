namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

public class RemoveGoodsIssueCommand : IRequest<bool>
{
    public string GoodsIssueId { get; set; }

    public RemoveGoodsIssueCommand(string goodsIssueId)
    {
        GoodsIssueId = goodsIssueId;
    }
}