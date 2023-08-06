namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

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
            throw new EntityNotFoundException(nameof(FinishedProductReceipt), request.FinishedProductReceiptId);
        }

        foreach (var entry in request.Entries)
        {
            var item = await _itemRepository.GetItemById(entry.ItemId, entry.Unit);
            if (item is null)
            {
                throw new EntityNotFoundException(nameof(Item), entry.ItemId + " with unit: " + entry.Unit);
            }

            var oldEntry = goodsReceipt.Entries.Find(e => e.PurchaseOrderNumber == entry.OldPurchaseOrderNumber
                                                                && e.Item == item);
            if (oldEntry is null)
            {
                throw new EntityNotFoundException(nameof(FinishedProductReceiptEntry), entry.ItemId + " with PO: " + entry.OldPurchaseOrderNumber);
            }
                         
            goodsReceipt.UpdateFinishedProductInventory(item, entry.OldPurchaseOrderNumber, entry.NewPurchaseOrderNumber, 
                entry.Quantity, goodsReceipt.Timestamp);

            goodsReceipt.UpdateReceiptEntry(oldEntry, entry.NewPurchaseOrderNumber ?? entry.OldPurchaseOrderNumber, entry.Quantity);
        }

        _finishedProductReceiptRepository.Update(goodsReceipt);
        return await _finishedProductReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
