using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

[DataContract]
public class UpdateConfirmedGoodsReceiptCommand : IRequest<bool>
{
    [DataMember]
    public string GoodsReceiptId { get; private set; }
    [DataMember]
    public List<UpdateConfirmedGoodsReceiptLotViewModel> GoodsReceiptLots { get; private set; }

    public UpdateConfirmedGoodsReceiptCommand(string goodsReceiptId, List<UpdateConfirmedGoodsReceiptLotViewModel> goodsReceiptLots)
    {
        GoodsReceiptId = goodsReceiptId;
        GoodsReceiptLots = goodsReceiptLots;
    }
}
