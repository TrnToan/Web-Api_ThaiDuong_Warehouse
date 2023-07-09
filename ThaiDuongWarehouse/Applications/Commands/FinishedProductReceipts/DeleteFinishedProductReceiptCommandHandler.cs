namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

public class DeleteFinishedProductReceiptCommandHandler : IRequestHandler<DeleteFinishedProductReceiptCommand, bool>
{
    private readonly IFinishedProductReceiptRepository _finishedProductReceiptRepository;

    public DeleteFinishedProductReceiptCommandHandler(IFinishedProductReceiptRepository finishedProductReceiptRepository)
    {
        _finishedProductReceiptRepository = finishedProductReceiptRepository;
    }

    public async Task<bool> Handle(DeleteFinishedProductReceiptCommand request, CancellationToken cancellationToken)
    {
        var goodsReceipt = await _finishedProductReceiptRepository.GetReceiptById(request.FinishedProductReceiptId);
        if (goodsReceipt is null)
        {
            throw new EntityNotFoundException($"GoodsReceipt with Id {request.FinishedProductReceiptId} not found.");
        }

        foreach (var entry in goodsReceipt.Entries)
        {
            goodsReceipt.AddLogEntry(entry.Item, -entry.Quantity, 0, goodsReceipt.Timestamp);
            goodsReceipt.RemoveFinishedProductInventory(entry.Item, entry.PurchaseOrderNumber, goodsReceipt.Timestamp);
        }

        _finishedProductReceiptRepository.Remove(goodsReceipt);
        return await _finishedProductReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
