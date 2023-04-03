using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

[DataContract]
public class ConfirmExportingGoodsIssueLotsCommand : IRequest<bool>
{
    public string goodsIssueId { get; private set; }
    public List<string> GoodsIssueLotIds { get; private set; }
    public ConfirmExportingGoodsIssueLotsCommand(string goodsIssueId, List<string> goodsIssueLotIds)
    {
        this.goodsIssueId = goodsIssueId;
        GoodsIssueLotIds = goodsIssueLotIds;
    }
}
