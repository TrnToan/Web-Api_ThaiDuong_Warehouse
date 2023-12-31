namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

// Trường hợp người dùng nhập sai mã sản phẩm thì phải xoá đi GoodsReceiptLot chứa sản phẩm đó trong phiếu rồi add lô mới
public class RemoveGoodsReceiptLotsCommandHandler : IRequestHandler<RemoveGoodsReceiptLotsCommand, bool>
{
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    private readonly IGoodsIssueRepository _goodsIssueRepository;
    private readonly IInventoryLogEntryRepository _inventoryLogEntryRepository;
    public RemoveGoodsReceiptLotsCommandHandler(IGoodsReceiptRepository goodsReceiptRepository, 
        IGoodsIssueRepository goodsIssueRepository,
        IInventoryLogEntryRepository inventoryLogEntryRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
        _goodsIssueRepository = goodsIssueRepository;
        _inventoryLogEntryRepository = inventoryLogEntryRepository;
    }

    public async Task<bool> Handle(RemoveGoodsReceiptLotsCommand request, CancellationToken cancellationToken)
    {
        var goodsReceipt = await _goodsReceiptRepository.GetGoodsReceiptByGoodsReceiptId(request.GoodsReceiptId);
        if (goodsReceipt is null)
        {
            throw new EntityNotFoundException($"GoodsReceipt with {request.GoodsReceiptId} doesn't exist");
        }

        var removedLots = new List<GoodsReceiptLot>();
        var logEntries = new List<InventoryLogEntry>();
        // Duyệt từng mã lô trong danh sách lô cần xoá
        foreach (string lotId in request.GoodsReceiptLotIds)
        {
            var lot = goodsReceipt.Lots.Find(l => l.GoodsReceiptLotId == lotId);
            if (lot == null)
            {
                throw new EntityNotFoundException($"GoodsReceiptLot with Id {lotId} does not exist.");
            }

            var isExportedLot = await _goodsIssueRepository.GetGoodsIssueLotById(lotId);
            if (isExportedLot is not null)
            {
                throw new ExportedItemLotException(isExportedLot.GoodsIssueLotId);
            }

            var logEntry = await _inventoryLogEntryRepository.GetLogEntry(lot.ItemId, lotId, goodsReceipt.Timestamp);

            logEntries.Add(logEntry);
            removedLots.Add(lot);
            goodsReceipt.RemoveLot(lot);    // Xoá lô khỏi lịch sử nhập
        }

        // InventoryLogEntry - DomainEvent
        var orderedReceiptLots = OrderGoodsReceiptLots(removedLots, logEntries);
        goodsReceipt.DeletedGoodsReceiptLotLogEntry(orderedReceiptLots);

        // Cập nhật lại phần tồn kho sau khi xoá lô - DomainEvent
        goodsReceipt.RemoveItemLotEntities(removedLots);
        _goodsReceiptRepository.Update(goodsReceipt);
        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }

    private IEnumerable<GoodsReceiptLot> OrderGoodsReceiptLots(List<GoodsReceiptLot> goodsReceiptLots, 
        List<InventoryLogEntry> logEntries)
    {
        return goodsReceiptLots.OrderBy(lot => logEntries.Find(e => e.ItemLotId == lot.GoodsReceiptLotId)?.TrackingTime);
    }
}
