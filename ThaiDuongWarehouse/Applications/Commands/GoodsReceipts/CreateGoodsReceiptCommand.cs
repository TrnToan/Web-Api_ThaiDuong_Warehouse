using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

[DataContract]
public class CreateGoodsReceiptCommand : IRequest<bool>
{
    [DataMember]
    public string GoodsReceiptId { get; private set; }
    [DataMember]
    public DateTime Timestamp { get; private set; }
    [DataMember]
    public EmployeeViewModel Employee { get; private set; }
    [DataMember]
    public List<GoodsReceiptLotViewModel> Lots { get; private set; }

    public CreateGoodsReceiptCommand(string goodsReceiptId, DateTime timestamp, 
        EmployeeViewModel employee, List<GoodsReceiptLotViewModel> lots)
    {
        GoodsReceiptId = goodsReceiptId;
        Timestamp = timestamp;
        Employee = employee;
        Lots = lots;
    }
}
