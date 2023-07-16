namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

[DataContract]
public class AddLotsToGoodsIssueCommand : IRequest<bool>
{
    [DataMember]
    public string GoodsIssueId { get; private set; }
    [DataMember]
    public List<CreateGoodsIssueLotViewModel> GoodsIssueLots { get; private set; }
    public AddLotsToGoodsIssueCommand(string goodsIssueId, List<CreateGoodsIssueLotViewModel> goodsIssueLots)
    {
        GoodsIssueId = goodsIssueId;
        GoodsIssueLots = goodsIssueLots;
    }
}
