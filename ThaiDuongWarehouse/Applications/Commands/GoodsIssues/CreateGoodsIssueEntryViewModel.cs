using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

[DataContract]
public class CreateGoodsIssueEntryViewModel
{
    [DataMember]
    public Item Item { get; private set; }
    [DataMember] 
    public string Unit { get; private set; }
    [DataMember]
    public double? RequestedSublotSize { get; private set; }
    [DataMember]
    public double RequestedQuantity { get; private set; }
    public CreateGoodsIssueEntryViewModel(Item item, string unit, double? requestedSublotSize ,double requestedQuantity)
    {
        Item = item;
        Unit = unit;
        RequestedSublotSize = requestedSublotSize;
        RequestedQuantity = requestedQuantity;
    }
}
