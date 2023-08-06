namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

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
            throw new EntityNotFoundException(nameof(FinishedProductReceipt), request.FinishedProductReceiptId);
        }

        foreach (var entry in request.Entries)
        {
            var item = await _itemRepository.GetItemById(entry.ItemId, entry.Unit);
            if (item is null)
            {
                throw new EntityNotFoundException(nameof(Item), entry.ItemId + " with unit: " + entry.Unit);
            }

            goodsReceipt.RemoveFinishedProductInventory(item, entry.PurchaseOrderNumber);
            goodsReceipt.RemoveReceiptEntry(item, entry.PurchaseOrderNumber);                    
        }

        _finishedProductReceiptRepository.Update(goodsReceipt);
        return await _finishedProductReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
