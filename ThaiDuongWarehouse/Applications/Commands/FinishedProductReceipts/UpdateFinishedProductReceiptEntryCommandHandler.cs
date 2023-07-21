﻿namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

public class UpdateFinishedProductReceiptEntryCommandHandler : IRequestHandler<UpdateFinishedProductReceiptEntryCommand, bool>
{
    private readonly IFinishedProductReceiptRepository _finishedProductReceiptRepository;
    private readonly IItemRepository _itemRepository;

    public UpdateFinishedProductReceiptEntryCommandHandler(IFinishedProductReceiptRepository finishedProductReceiptRepository, 
        IItemRepository itemRepository)
    {
        _finishedProductReceiptRepository = finishedProductReceiptRepository;
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(UpdateFinishedProductReceiptEntryCommand request, CancellationToken cancellationToken)
    {
        var goodsReceipt = await _finishedProductReceiptRepository.GetReceiptById(request.FinishedProductReceiptId);
        if (goodsReceipt is null)
        {
            throw new EntityNotFoundException($"FinishedProductReceipt, {request.FinishedProductReceiptId}");
        }

        foreach (var entry in request.Entries)
        {
            var item = await _itemRepository.GetItemById(entry.ItemId, entry.Unit);
            if (item is null)
            {
                throw new EntityNotFoundException($"Item, {entry.ItemId}");
            }

            var oldEntry = goodsReceipt.Entries.FirstOrDefault(e => e.PurchaseOrderNumber == entry.OldPurchaseOrderNumber
                                                                && e.Item == item);
            if (oldEntry is null)
            {
                throw new EntityNotFoundException($"Entry, {entry.ItemId} & {entry.OldPurchaseOrderNumber}");
            }

            //double changedQuantity = entry.Quantity - oldEntry.Quantity;
            goodsReceipt.UpdateReceiptEntry(oldEntry, entry.NewPurchaseOrderNumber, entry.Quantity);
            //if (changedQuantity != 0)
            //{
            //    // Ghi đè quantity của entry mới lên quantity của entry cũ 
            //    goodsReceipt.UpdateQuantityLogEntry(entry.OldPurchaseOrderNumber, item.Id, entry.Quantity, entry.Quantity, 
            //        goodsReceipt.Timestamp);
            //}
             
            //if (entry.NewPurchaseOrderNumber != entry.OldPurchaseOrderNumber)
            //{
            //    goodsReceipt.ModifyLogEntry(entry.OldPurchaseOrderNumber, entry.NewPurchaseOrderNumber, item.Id, 
            //        goodsReceipt.Timestamp);
            //}
            goodsReceipt.UpdateFinishedProductInventory(item, entry.OldPurchaseOrderNumber, entry.NewPurchaseOrderNumber, 
                entry.Quantity, goodsReceipt.Timestamp);
        }

        _finishedProductReceiptRepository.Update(goodsReceipt);
        return await _finishedProductReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
