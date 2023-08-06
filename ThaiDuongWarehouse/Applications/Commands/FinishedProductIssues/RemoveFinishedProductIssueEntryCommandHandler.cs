namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductIssues;

public class RemoveFinishedProductIssueEntryCommandHandler : IRequestHandler<RemoveFinishedProductIssueEntryCommand, bool>
{
    private readonly IFinishedProductIssueRepository _finishedProductIssueRepository;
    private readonly IItemRepository _itemRepository;

    public RemoveFinishedProductIssueEntryCommandHandler(IFinishedProductIssueRepository finishedProductIssueRepository, 
        IItemRepository itemRepository)
    {
        _finishedProductIssueRepository = finishedProductIssueRepository;
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(RemoveFinishedProductIssueEntryCommand request, CancellationToken cancellationToken)
    {
        var productIssue = await _finishedProductIssueRepository.GetIssueById(request.FinishedProductIssueId);
        if (productIssue is null)
        {
            throw new EntityNotFoundException(nameof(FinishedProductIssue), request.FinishedProductIssueId);
        }

        var item = await _itemRepository.GetItemById(request.ItemId, request.Unit);
        if (item is null)
        {
            throw new EntityNotFoundException(nameof(Item), request.ItemId + "with unit: " + request.Unit);
        }

        productIssue.RestoreProductInventory(item, request.PurchaseOrderNumber);
        productIssue.RemoveIssueEntry(request.ItemId, request.PurchaseOrderNumber);

        return await _finishedProductIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
