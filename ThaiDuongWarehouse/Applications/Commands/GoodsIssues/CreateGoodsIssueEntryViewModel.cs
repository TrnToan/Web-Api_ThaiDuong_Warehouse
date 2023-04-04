using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

[DataContract]
public class CreateGoodsIssueEntryViewModel
{
    [DataMember]
    public string ItemId { get; private set; }
    [DataMember] 
    public string Unit { get; private set; }
    [DataMember]
    public double? RequestedSublotSize { get; private set; }
    [DataMember]
    public double RequestedQuantity { get; private set; }
    public CreateGoodsIssueEntryViewModel(string itemId, string unit, double? requestedSublotSize ,double requestedQuantity)
    {
        ItemId = itemId;
        Unit = unit;
        RequestedSublotSize = requestedSublotSize;
        RequestedQuantity = requestedQuantity;
    }
}
