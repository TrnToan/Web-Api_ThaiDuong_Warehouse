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
            throw new EntityNotFoundException(nameof(FinishedProductReceipt), request.FinishedProductReceiptId);
        }

        foreach (var entry in goodsReceipt.Entries)
        {
            goodsReceipt.RemoveFinishedProductInventory(entry.Item, entry.PurchaseOrderNumber);
        }

        _finishedProductReceiptRepository.Remove(goodsReceipt);
        return await _finishedProductReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
