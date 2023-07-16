﻿namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

public class RemoveFinishedProductReceiptEntriesCommandHandler : IRequestHandler<RemoveFinishedProductReceiptEntriesCommand, bool>
{
    private readonly IFinishedProductReceiptRepository _finishedProductReceiptRepository;
    private readonly IItemRepository _itemRepository;

    public RemoveFinishedProductReceiptEntriesCommandHandler(IFinishedProductReceiptRepository finishedProductReceiptRepository, IItemRepository itemRepository)
    {
        _finishedProductReceiptRepository = finishedProductReceiptRepository;
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(RemoveFinishedProductReceiptEntriesCommand request, CancellationToken cancellationToken)
    {
        var goodsReceipt = await _finishedProductReceiptRepository.GetReceiptById(request.FinishedProductReceiptId);
        if (goodsReceipt is null)
        {
            throw new EntityNotFoundException($"GoodsReceipt {request.FinishedProductReceiptId} not found.");
        }

        foreach (var entry in request.Entries)
        {
            var item = await _itemRepository.GetItemById(entry.ItemId, entry.Unit);
            if (item is null)
            {
                throw new EntityNotFoundException($"Item, {entry.ItemId}");
            }

            var removedEntry = goodsReceipt.Entries.FirstOrDefault(e => e.Item == item && e.PurchaseOrderNumber == entry.PurchaseOrderNumber);

            goodsReceipt.RemoveReceiptEntry(item, entry.PurchaseOrderNumber);
            
            // ChangedQuantity = - ChangedQuantity của entry đó khi xoá entry trong trường hợp nhập liệu sai 
            goodsReceipt.AddLogEntry(item, -removedEntry.Quantity, 0, goodsReceipt.Timestamp);
            goodsReceipt.RemoveFinishedProductInventory(item, entry.PurchaseOrderNumber, goodsReceipt.Timestamp);
        }

        _finishedProductReceiptRepository.Update(goodsReceipt);
        return await _finishedProductReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
