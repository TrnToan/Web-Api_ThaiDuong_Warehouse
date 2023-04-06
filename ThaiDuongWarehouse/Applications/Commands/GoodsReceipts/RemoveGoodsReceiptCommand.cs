using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

[DataContract]
public class RemoveGoodsReceiptCommand : IRequest<bool>
{
	[DataMember]
    public string GoodsReceiptId { get; private set; }
	public RemoveGoodsReceiptCommand(string goodsReceiptId)
	{
		GoodsReceiptId = goodsReceiptId;
	}
}
