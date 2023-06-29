using System.Runtime.Serialization;
namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

[DataContract]
public class CreateGoodsReceiptCommand : IRequest<bool>
{
    [DataMember]
    public string GoodsReceiptId { get; set; }
    [DataMember]
    public string? Supplier { get; set; }
    [DataMember]
    public string EmployeeId { get; set; }
    [DataMember]
    public List<CreateGoodsReceiptLotViewModel> GoodsReceiptLots { get; set; }

    public CreateGoodsReceiptCommand(string goodsReceiptId, string supplier,
        string employeeId, List<CreateGoodsReceiptLotViewModel> goodsReceiptLots)
    {
        GoodsReceiptId = goodsReceiptId;
        Supplier = supplier;
        EmployeeId = employeeId;
        GoodsReceiptLots = goodsReceiptLots;
    }
}
