namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

[DataContract]
public class CreateLotAdjustmentCommand : IRequest<bool>
{
    [DataMember]
    public string LotId { get; private set; }
    [DataMember]
    public string ItemId { get; private set; }
    [DataMember]
    public double BeforeQuantity { get; private set; }
    [DataMember] 
    public double AfterQuantity { get; private set; }
    [DataMember]
    public string Unit { get; private set; }
    [DataMember]
    public string EmployeeName { get; private set; }
    [DataMember]
    public string? Note { get; private set; }
    [DataMember]
    public List<SublotAdjustmentViewModel> SublotAdjustments { get; private set; }

    public CreateLotAdjustmentCommand(string lotId, string itemId, double beforeQuantity, 
        double afterQuantity, string unit, string employeeName, string? note, List<SublotAdjustmentViewModel> sublotAdjustments)
    {
        LotId = lotId;
        ItemId = itemId;
        BeforeQuantity = beforeQuantity;
        AfterQuantity = afterQuantity;
        Unit = unit;
        EmployeeName = employeeName;
        Note = note;
        SublotAdjustments = sublotAdjustments;
    }
}
