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
            throw new EntityNotFoundException($"GoodsReceipt with {request.GoodsReceiptId} doesn't exist");
        }
        if (goodsReceipt.IsConfirmed)
        {
            throw new NotAllowedToDeleteException($"Cannot delete confirmed GoodsReceipt.");
        }

        _goodsReceiptRepository.Remove(goodsReceipt);

        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
