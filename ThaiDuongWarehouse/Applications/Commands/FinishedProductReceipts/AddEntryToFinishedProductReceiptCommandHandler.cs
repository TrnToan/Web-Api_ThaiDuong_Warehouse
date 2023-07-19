namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

public class AddEntryToFinishedProductReceiptCommandHandler : IRequestHandler<AddEntryToFinishedProductReceiptCommand, bool>
{
    private readonly IFinishedProductReceiptRepository _finishedProductReceiptRepository;
    private readonly IItemRepository _itemRepository;

    public AddEntryToFinishedProductReceiptCommandHandler(IFinishedProductReceiptRepository finishedProductReceiptRepository,
        IItemRepository itemRepository)
    {
        _finishedProductReceiptRepository = finishedProductReceiptRepository;
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(AddEntryToFinishedProductReceiptCommand request, CancellationToken cancellationToken)
    {
        var goodsReceipt = await _finishedProductReceiptRepository.GetReceiptById(request.FinishedProductReceiptId);
        if (goodsReceipt is null)
        {
            throw new EntityNotFoundException($"FinishedProductReceipt with Id {request.FinishedProductReceiptId}.");
        }

        foreach (var addedEntry in request.Entries)
        {
            var item = await _itemRepository.GetItemById(addedEntry.ItemId, addedEntry.Unit);
            if (item is null)
            {
                throw new EntityNotFoundException($"Item, {addedEntry.ItemId}");
            }

            var finishedProductEntry = new FinishedProductReceiptEntry(addedEntry.PurchaseOrderNumber, addedEntry.Quantity,
                addedEntry.Note, goodsReceipt.Id, item);

            goodsReceipt.AddReceiptEntry(finishedProductEntry);

            // ReceivedQuantity = ChangedQuantity = ChangedQuantity của entry mới thêm vào trong trường hợp bổ sung entry
            goodsReceipt.AddLogEntry(addedEntry.PurchaseOrderNumber, item.Id, addedEntry.Quantity, addedEntry.Quantity, 
                goodsReceipt.Timestamp);
            goodsReceipt.AddFinishedProductInventory(item, addedEntry.PurchaseOrderNumber, addedEntry.Quantity, 
                goodsReceipt.Timestamp);
        }

        _finishedProductReceiptRepository.Update(goodsReceipt);
        return await _finishedProductReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
