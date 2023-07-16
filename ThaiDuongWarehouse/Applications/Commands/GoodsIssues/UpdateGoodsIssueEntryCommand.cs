namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

[DataContract]
public class UpdateGoodsIssueEntryCommand : IRequest<bool>
{
    [DataMember]
    public string GoodsIssueId { get; private set; }
    [DataMember]
    public string ItemId { get; private set; }
    [DataMember]
    public string Unit { get; private set; }
    [DataMember]
    public double RequestedQuantity { get; private set; }

    public UpdateGoodsIssueEntryCommand(string goodsIssueId, string itemId, string unit, double requestedQuantity)
    {
        GoodsIssueId = goodsIssueId;
        ItemId = itemId;
        Unit = unit;
        RequestedQuantity = requestedQuantity;
    }
}
