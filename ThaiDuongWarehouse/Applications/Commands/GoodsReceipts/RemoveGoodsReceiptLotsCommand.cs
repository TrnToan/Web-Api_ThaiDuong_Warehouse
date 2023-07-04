using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

[DataContract]
public class RemoveGoodsReceiptLotsCommand : IRequest<bool>
{
	[DataMember]
    public string GoodsReceiptId { get; private set; }
	public List<string> GoodsReceiptLotIds { get; private set; }
	public RemoveGoodsReceiptLotsCommand(string goodsReceiptId, List<string> goodsReceiptLotIds)
    {
        GoodsReceiptId = goodsReceiptId;
        GoodsReceiptLotIds = goodsReceiptLotIds;
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public RemoveGoodsReceiptLotsCommand()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {	}
}
