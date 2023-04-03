using System.Runtime.Serialization;
namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

[DataContract]
public class CreateGoodsReceiptCommand : IRequest<bool>
{
    [DataMember]
    public string GoodsReceiptId { get; private set; }
    [DataMember]
    public DateTime Timestamp { get; private set; } = DateTime.Now;
    [DataMember]
    public string? Supplier { get; private set; }
    [DataMember]
    public string EmployeeId { get; private set; }
    [DataMember]
    public List<CreateGoodsReceiptLotViewModel> GoodsReceiptLots { get; private set; }

    public CreateGoodsReceiptCommand(string goodsReceiptId, string supplier,
        string employeeId, List<CreateGoodsReceiptLotViewModel> lots)
    {
        GoodsReceiptId = goodsReceiptId;
        //Timestamp = timestamp;
        Supplier = supplier;
        EmployeeId = employeeId;
        GoodsReceiptLots = lots;
    }
}
