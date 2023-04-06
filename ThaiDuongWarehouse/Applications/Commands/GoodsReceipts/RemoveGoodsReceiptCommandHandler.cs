namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class RemoveGoodsReceiptCommandHandler : IRequestHandler<RemoveGoodsReceiptCommand, bool>
{
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    public RemoveGoodsReceiptCommandHandler(IGoodsReceiptRepository goodsReceiptRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
    }

    public async Task<bool> Handle(RemoveGoodsReceiptCommand request, CancellationToken cancellationToken)
    {
        var goodsReceipt = await _goodsReceiptRepository.GetGoodsReceiptById(request.GoodsReceiptId);
        if (goodsReceipt is null)
        {
            throw new EntityNotFoundException($"{goodsReceipt} with {request.GoodsReceiptId} doesn't exist");
        }

        _goodsReceiptRepository.Remove(goodsReceipt);

        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync();
    }
}
