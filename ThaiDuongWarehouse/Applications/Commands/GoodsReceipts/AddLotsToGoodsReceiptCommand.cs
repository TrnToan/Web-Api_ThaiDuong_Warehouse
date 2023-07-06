namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

[DataContract]
public class AddLotsToGoodsReceiptCommand : IRequest<bool>
{
    [DataMember]
    public string GoodsReceiptId { get; private set; }
    [DataMember]
    public List<CreateGoodsReceiptLotViewModel> GoodsReceiptLots { get; private set; }

    public AddLotsToGoodsReceiptCommand(string goodsReceiptId, List<CreateGoodsReceiptLotViewModel> goodsReceiptLots)
    {
        GoodsReceiptId = goodsReceiptId;
        GoodsReceiptLots = goodsReceiptLots;
    }
}
