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
    public string? Supplier { get; private set; }
    [DataMember]
    public List<CreateGoodsReceiptLotViewModel> GoodsReceiptLots { get; private set; }
    [DataMember]
    public Employee Employee { get; private set; }
    
    public CreateGoodsReceiptCommand(string goodsReceiptId, DateTime timestamp, string supplier,
        Employee employee, List<CreateGoodsReceiptLotViewModel> lots)
    {
        GoodsReceiptId = goodsReceiptId;
        Timestamp = timestamp;
        Supplier = supplier;
        Employee = employee;
        GoodsReceiptLots = lots;
    }
}
