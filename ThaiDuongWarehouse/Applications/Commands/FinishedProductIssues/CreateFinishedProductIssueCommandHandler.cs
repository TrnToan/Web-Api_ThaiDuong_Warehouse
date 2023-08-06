using ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductInventoryAggregate;

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
        var existedGoodsIssue = await _finisedProductIssueRepository.GetIssueById(request.FinishedProductIssueId);
        if (existedGoodsIssue is not null)
        {
            throw new DuplicateRecordException(nameof(FinishedProductIssue), existedGoodsIssue.FinishedProductIssueId);
        }

        var employee = await _employeeRepository.GetEmployeeById(request.EmployeeId);
        if (employee is null)
        {
            throw new EntityNotFoundException(nameof(Employee), request.EmployeeId);
        }

        var productIssue = new FinishedProductIssue(request.FinishedProductIssueId, request.Receiver, DateTime.UtcNow.AddHours(7), 
            employee.Id);
        foreach (var entry in request.Entries)
        {
            var item = await _itemRepository.GetItemById(entry.ItemId, entry.Unit);
            if (item is null)
            {
                throw new EntityNotFoundException(nameof(Item), entry.ItemId);
            }

            var finishedProductIssueEntry = new FinishedProductIssueEntry(entry.PurchaseOrderNumber, entry.Quantity,
                entry.Note, productIssue.Id, item);
            productIssue.AddIssueEntry(finishedProductIssueEntry);

            productIssue.UpdateFinishedProductInventory(item, entry.PurchaseOrderNumber, entry.Quantity);
        }
        await _finisedProductIssueRepository.AddAsync(productIssue);
        return await _finisedProductIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
