using System.Runtime.Serialization;
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
    public string OldPurchaseOrderNumber { get; private set; }
    [DataMember]
    public string NewPurchaseOrderNumber { get; private set; }
    [DataMember]
    public string EmployeeName { get; private set; }
    [DataMember]
    public string? Note { get; private set; }

    public CreateLotAdjustmentCommand(string lotId, string itemId, double beforeQuantity, 
        double afterQuantity, string oldPurchaseOrderNumber, string newPurchaseOrderNumber, 
        string employeeName, string? note)
    {
        LotId = lotId;
        ItemId = itemId;
        BeforeQuantity = beforeQuantity;
        AfterQuantity = afterQuantity;
        OldPurchaseOrderNumber = oldPurchaseOrderNumber;
        NewPurchaseOrderNumber = newPurchaseOrderNumber;
        EmployeeName = employeeName;
        Note = note;
    }
}
