using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

[DataContract]
public class CreateGoodsIssueLotViewModel
{
    [DataMember]
    public string GoodsIssueLotId { get; private set; }
    [DataMember]
    public string ItemId { get; private set; }
    [DataMember]
    public double Quantity { get; private set; }
    [DataMember]
    public double? SublotSize { get; private set; }
    [DataMember]
    public string? SublotUnit { get; private set; }
    [DataMember]
    public string? Note { get; private set; }
    [DataMember]
    public string EmployeeId { get; private set; }
    public CreateGoodsIssueLotViewModel(string goodsIssueLotId, string itemId, double quantity, double? sublotSize, string? sublotUnit,
        string? note, string employeeId)
    {
        GoodsIssueLotId = goodsIssueLotId;
        ItemId = itemId;
        Quantity = quantity;
        SublotSize = sublotSize;
        SublotUnit = sublotUnit;
        Note = note;
        EmployeeId = employeeId;
        SublotUnit = sublotUnit;
    }
}
