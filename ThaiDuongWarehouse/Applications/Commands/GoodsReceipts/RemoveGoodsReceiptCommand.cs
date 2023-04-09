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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public RemoveGoodsReceiptCommand()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {	}
}
