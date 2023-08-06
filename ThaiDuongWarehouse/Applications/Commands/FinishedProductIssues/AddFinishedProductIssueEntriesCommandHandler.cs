namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductIssues;

public class AddFinishedProductIssueEntriesCommandHandler : IRequestHandler<AddFinishedProductIssueEntriesCommand, bool>
{
    private readonly IFinishedProductIssueRepository _finishedProductIssueRepository;
    private readonly IItemRepository _itemRepository;

    public AddFinishedProductIssueEntriesCommandHandler(IFinishedProductIssueRepository finishedProductIssueRepository, 
        IItemRepository itemRepository)
    {
        _finishedProductIssueRepository = finishedProductIssueRepository;
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(AddFinishedProductIssueEntriesCommand request, CancellationToken cancellationToken)
    {
        var productIssue = await _finishedProductIssueRepository.GetIssueById(request.FinishedProductIssueId);
        if (productIssue is null)
        {
            throw new EntityNotFoundException(nameof(FinishedProductIssue), request.FinishedProductIssueId);
        }

        foreach (var entry in request.Entries)
        {
            var item = await _itemRepository.GetItemById(entry.ItemId, entry.Unit);
            if (item is null)
            {
                throw new EntityNotFoundException(nameof(Item), entry.ItemId + "with unit: " + entry.Unit);
            }

            FinishedProductIssueEntry addedEntry = new (entry.PurchaseOrderNumber, entry.Quantity, entry.Note, productIssue.Id, 
                item);

            productIssue.AddIssueEntry(addedEntry);
            productIssue.UpdateFinishedProductInventory(item, entry.PurchaseOrderNumber, entry.Quantity);
        }

        _finishedProductIssueRepository.Update(productIssue);
        return await _finishedProductIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
