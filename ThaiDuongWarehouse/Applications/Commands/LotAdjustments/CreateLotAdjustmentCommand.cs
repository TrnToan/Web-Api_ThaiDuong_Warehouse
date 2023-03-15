using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

[DataContract]
public class CreateLotAdjustmentCommand : IRequest<bool>
{
    [DataMember]
    public string LotId { get; private set; }
    [DataMember]
    public string NewPurchaseOrderNumber { get; private set; }
    [DataMember]
    public string OldPurchaseOrderNumber { get; private set; }
    [DataMember]
    public string? Note { get; private set; }
    [DataMember]
    public double BeforeQuantity { get; private set; }
    [DataMember]
    public double AfterQuantity { get; private set; }
    [DataMember]
    public bool IsConfirmed { get; private set; } = false;
    [DataMember]
    public DateTime Timestamp { get; private set; }
    [DataMember]
    public Employee Employee { get; private set; }
    [DataMember]
    public Item Item { get; private set; }

    public CreateLotAdjustmentCommand(string lotId, string newPurchaseOrderNumber, string oldPurchaseOrderNumber, 
        string? note, double beforeQuantity, double afterQuantity, DateTime timestamp, 
        Employee employee, Item item)
    {
        LotId = lotId;
        NewPurchaseOrderNumber = newPurchaseOrderNumber;
        OldPurchaseOrderNumber = oldPurchaseOrderNumber;
        Note = note;
        BeforeQuantity = beforeQuantity;
        AfterQuantity = afterQuantity;
        Timestamp = timestamp;
        Employee = employee;
        Item = item;
    }
}
