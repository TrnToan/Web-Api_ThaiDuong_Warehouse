namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

public class CreateFinishedProductReceiptCommandHandler : IRequestHandler<CreateFinishedProductReceiptCommand, bool>
{
    private readonly IFinishedProductReceiptRepository _finishedProductReceiptRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IItemRepository _itemRepository;

    public CreateFinishedProductReceiptCommandHandler(IFinishedProductReceiptRepository finishedProductReceiptRepository,
        IEmployeeRepository employeeRepository, IItemRepository itemRepository)
    {
        _finishedProductReceiptRepository = finishedProductReceiptRepository;
        _employeeRepository = employeeRepository;
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(CreateFinishedProductReceiptCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployeeById(request.EmployeeId);
        if (employee is null)
        {
            throw new EntityNotFoundException($"Employee with Id {request.EmployeeId} does not exist.");
        }
        
        var finishedProductReceipt = new FinishedProductReceipt(request.FinishedProductReceiptId, DateTime.UtcNow.AddHours(7), employee.Id);
        foreach (var entry in request.Entries)
        {
            var item = await _itemRepository.GetItemById(entry.ItemId, entry.Unit);
            if (item is null)
            {
                throw new EntityNotFoundException($"Item with Id {entry.ItemId} does not exist.");
            }

            var finishedProductReceiptEntry = new FinishedProductReceiptEntry(entry.PurchaseOrderNumber, entry.Quantity,
                entry.Note, finishedProductReceipt.Id, item);
            finishedProductReceipt.AddReceiptEntry(finishedProductReceiptEntry);

            finishedProductReceipt.AddFinishedProductInventory(item, entry.PurchaseOrderNumber, entry.Quantity, finishedProductReceipt.Timestamp);                       
        }

        await _finishedProductReceiptRepository.Add(finishedProductReceipt);
        return await _finishedProductReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
