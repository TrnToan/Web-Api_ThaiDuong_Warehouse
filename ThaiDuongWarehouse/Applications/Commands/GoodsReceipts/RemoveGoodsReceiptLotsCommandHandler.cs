namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class RemoveGoodsReceiptLotsCommandHandler : IRequestHandler<RemoveGoodsReceiptLotsCommand, bool>
{
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    public RemoveGoodsReceiptLotsCommandHandler(IGoodsReceiptRepository goodsReceiptRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
    }

    public async Task<bool> Handle(RemoveGoodsReceiptLotsCommand request, CancellationToken cancellationToken)
    {
        var goodsReceipt = await _goodsReceiptRepository.GetGoodsReceiptById(request.GoodsReceiptId);
        if (goodsReceipt is null)
        {
            throw new EntityNotFoundException($"GoodsReceipt with {request.GoodsReceiptId} doesn't exist");
        }

        var removedLots = new List<GoodsReceiptLot>();
        foreach (string lotId in request.GoodsReceiptLotIds)
        {
            var lot = goodsReceipt.Lots.FirstOrDefault(l => l.GoodsReceiptLotId == lotId);
            if (lot == null)
            {
                throw new EntityNotFoundException($"GoodsReceiptLot with Id {lotId} does not exist.");
            }
            removedLots.Add(lot);

            goodsReceipt.RemoveLot(lot);
            goodsReceipt.AddDeletedGoodsReceiptLotLogEntry(lotId, lot.ItemId, -lot.Quantity, goodsReceipt.Timestamp);
        }

        goodsReceipt.RemoveItemLotEntities(removedLots);
        _goodsReceiptRepository.Update(goodsReceipt);
        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
