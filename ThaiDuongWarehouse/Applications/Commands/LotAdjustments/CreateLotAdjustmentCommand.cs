namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

[DataContract]
public class CreateLotAdjustmentCommand : IRequest<bool>
{
    [DataMember]
    public string LotId { get; private set; }
    [DataMember]
    public string ItemId { get; private set; }
    [DataMember] 
    public double AfterQuantity { get; private set; }
    [DataMember]
    public string Unit { get; private set; }
    [DataMember]
    public string EmployeeName { get; private set; }
    [DataMember]
    public string? Note { get; private set; }
    [DataMember]
    public List<CreateSublotAdjustmentViewModel> SublotAdjustments { get; private set; }

    public CreateLotAdjustmentCommand(string lotId, string itemId, double afterQuantity, string unit, string employeeName, 
        string? note, List<CreateSublotAdjustmentViewModel> sublotAdjustments)
    {
        LotId = lotId;
        ItemId = itemId;
        AfterQuantity = afterQuantity;
        Unit = unit;
        EmployeeName = employeeName;
        Note = note;
        SublotAdjustments = sublotAdjustments;
    }
}
