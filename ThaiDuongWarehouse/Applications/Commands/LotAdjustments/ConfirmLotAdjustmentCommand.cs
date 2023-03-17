using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

[DataContract]
public class ConfirmLotAdjustmentCommand : IRequest<bool>
{
    [DataMember]
    public string LotId { get; private set; }
    [DataMember]
    public string ItemId { get; private set; }
    [DataMember]
    public double AfterQuantity { get; private set; }
    [DataMember]
    public string NewPurchaseOrderNumber { get; private set; }
    [DataMember]
    public string EmployeeId { get; private set; }
    [DataMember]
    public DateTime Timestamp { get; private set; }
    [DataMember]
    public string? Note { get; private set; }
    public ConfirmLotAdjustmentCommand(string lotId, string itemId, double beforeQuantity, double afterQuantity, 
        string oldPurchaseOrderNumber, string newPurchaseOrderNumber, string employeeId, DateTime timestamp, string? note)
    {
        LotId = lotId;
        ItemId = itemId;
        AfterQuantity = afterQuantity;
        NewPurchaseOrderNumber = newPurchaseOrderNumber;
        EmployeeId = employeeId;
        Timestamp = timestamp;
        Note = note;
    }
}
