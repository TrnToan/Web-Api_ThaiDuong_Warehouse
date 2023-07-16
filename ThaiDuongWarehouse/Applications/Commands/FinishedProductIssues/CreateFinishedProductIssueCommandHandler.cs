using ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductIssueAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductIssues;

public class CreateFinishedProductIssueCommandHandler : IRequestHandler<CreateFinishedProductIssueCommand, bool>
{
    private readonly IFinishedProductIssueRepository _finisedProductIssueRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IItemRepository _itemRepository;

    public CreateFinishedProductIssueCommandHandler(IFinishedProductIssueRepository finisedProductIssueRepository, 
        IEmployeeRepository employeeRepository, IItemRepository itemRepository)
    {
        _finisedProductIssueRepository = finisedProductIssueRepository;
        _employeeRepository = employeeRepository;
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(CreateFinishedProductIssueCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployeeById(request.EmployeeId);
        if (employee is null)
        {
            throw new EntityNotFoundException($"Employee with Id {request.EmployeeId} does not exist.");
        }

        var productIssue = new FinishedProductIssue(request.FinishedProductIssueId, request.Receiver, request.Timestamp, employee.Id);
        foreach (var entry in request.Entries)
        {
            var item = await _itemRepository.GetItemById(entry.ItemId, entry.Unit);
            if (item is null)
            {
                throw new EntityNotFoundException($"Item with Id {entry.ItemId} does not exist.");
            }

            var finishedProductIssueEntry = new FinishedProductIssueEntry(entry.PurchaseOrderNumber, entry.Quantity,
                entry.Note, productIssue.Id, item);
            productIssue.AddIssueEntry(finishedProductIssueEntry);

            productIssue.UpdateFinishedProductInventory(item, entry.PurchaseOrderNumber, entry.Quantity, productIssue.Timestamp);
        }
        return await _finisedProductIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
