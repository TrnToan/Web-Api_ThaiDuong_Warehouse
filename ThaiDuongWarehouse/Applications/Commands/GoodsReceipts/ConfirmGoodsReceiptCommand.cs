using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

[DataContract]
public class ConfirmGoodsReceiptCommand : IRequest<bool>
{
    [DataMember]
    public string GoodsReceiptId { get; private set; }
	public ConfirmGoodsReceiptCommand(string goodsReceiptId)
    {
        GoodsReceiptId = goodsReceiptId;
    }
}
