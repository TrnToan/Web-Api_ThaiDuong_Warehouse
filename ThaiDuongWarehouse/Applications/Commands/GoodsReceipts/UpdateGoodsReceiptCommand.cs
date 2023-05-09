using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

[DataContract]
public class UpdateGoodsReceiptCommand : IRequest<bool>
{
    [DataMember]
    public string GoodsReceiptId { get; private set; }
    [DataMember]
    public List<UpdateGoodsReceiptLotViewModel> GoodsReceiptLots { get; private set; }

    public UpdateGoodsReceiptCommand(string goodsReceiptId, List<UpdateGoodsReceiptLotViewModel> goodsReceiptLots)
    {
        GoodsReceiptId = goodsReceiptId;
        GoodsReceiptLots = goodsReceiptLots;
    }
}
