namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

[DataContract]
public class UpdateGoodsIssueEntryCommand : IRequest<bool>
{
    [DataMember]
    public string GoodsIssueId { get; private set; }
    [DataMember]
    public List<UpdateGoodsIssueEntryViewModel> Entries { get; private set; }

    public UpdateGoodsIssueEntryCommand(string goodsIssueId, List<UpdateGoodsIssueEntryViewModel> entries)
    {
        GoodsIssueId = goodsIssueId;
        Entries = entries;
    }
}
