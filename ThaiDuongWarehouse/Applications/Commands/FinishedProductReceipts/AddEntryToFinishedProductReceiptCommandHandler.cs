using ThaiDuongWarehouse.Domain.Seedwork;

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
            throw new EntityNotFoundException(nameof(FinishedProductReceipt), request.FinishedProductReceiptId);
        }

        foreach (var addedEntry in request.Entries)
        {
            var item = await _itemRepository.GetItemById(addedEntry.ItemId, addedEntry.Unit);
            if (item is null)
            {
                throw new EntityNotFoundException(nameof(Item), addedEntry.ItemId + " with unit: " + addedEntry.Unit);
            }

            var finishedProductEntry = new FinishedProductReceiptEntry(addedEntry.PurchaseOrderNumber, addedEntry.Quantity,
                addedEntry.Note, goodsReceipt.Id, item);

            goodsReceipt.AddReceiptEntry(finishedProductEntry);

            goodsReceipt.AddFinishedProductInventory(item, addedEntry.PurchaseOrderNumber, addedEntry.Quantity, 
                goodsReceipt.Timestamp);
        }

        _finishedProductReceiptRepository.Update(goodsReceipt);
        return await _finishedProductReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
