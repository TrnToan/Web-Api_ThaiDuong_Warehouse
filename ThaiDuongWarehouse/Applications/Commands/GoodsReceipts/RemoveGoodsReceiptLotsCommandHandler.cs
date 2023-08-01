namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

// Trường hợp người dùng nhập sai mã sản phẩm thì phải xoá đi GoodsReceiptLot chứa sản phẩm đó trong phiếu rồi add lô mới
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
        // Duyệt từng mã lô trong danh sách lô cần xoá
        foreach (string lotId in request.GoodsReceiptLotIds)
        {
            var lot = goodsReceipt.Lots.Find(l => l.GoodsReceiptLotId == lotId);
            if (lot == null)
            {
                throw new EntityNotFoundException($"GoodsReceiptLot with Id {lotId} does not exist.");
            }
            removedLots.Add(lot);

            goodsReceipt.RemoveLot(lot);    // Xoá lô khỏi lịch sử nhập
            // InventoryLogEntry - DomainEvent
            goodsReceipt.DeletedGoodsReceiptLotLogEntry(lot.ItemId, lotId, goodsReceipt.Timestamp);
        }

        // Cập nhật lại phần tồn kho sau khi xoá lô - DomainEvent
        goodsReceipt.RemoveItemLotEntities(removedLots);
        _goodsReceiptRepository.Update(goodsReceipt);
        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
