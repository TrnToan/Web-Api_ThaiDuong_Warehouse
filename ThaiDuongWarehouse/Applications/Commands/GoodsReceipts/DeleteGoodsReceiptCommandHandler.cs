namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class DeleteGoodsReceiptCommandHandler : IRequestHandler<DeleteGoodsReceiptCommand, bool>
{
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;

    public DeleteGoodsReceiptCommandHandler(IGoodsReceiptRepository goodsReceiptRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
    }

    public async Task<bool> Handle(DeleteGoodsReceiptCommand request, CancellationToken cancellationToken)
    {
        var removedGoodsReceipt = await _goodsReceiptRepository.GetGoodsReceiptById(request.GoodsReceiptId);
        if (removedGoodsReceipt is null)
        {
            throw new EntityNotFoundException(nameof(GoodsReceipt), request.GoodsReceiptId);
        }

        foreach (var lot in removedGoodsReceipt.Lots)
        {
            removedGoodsReceipt.DeletedGoodsReceiptLotLogEntry(lot.ItemId, lot.GoodsReceiptLotId, removedGoodsReceipt.Timestamp);
        }
        removedGoodsReceipt.RemoveItemLotEntities(removedGoodsReceipt.Lots);

        _goodsReceiptRepository.Remove(removedGoodsReceipt);
        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
