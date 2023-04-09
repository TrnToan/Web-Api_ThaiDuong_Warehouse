
namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

public class CreateLotAdjustmentCommandHandler : IRequestHandler<CreateLotAdjustmentCommand, bool>
{
    private readonly ILotAdjustmentRepository _lotAdjustmentRepository;
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IItemRepository _itemRepository;
    public CreateLotAdjustmentCommandHandler(ILotAdjustmentRepository lotAdjustmentRepository, IItemLotRepository itemLotRepository, 
        IEmployeeRepository employeeRepository, IItemRepository itemRepository)
    {
        _lotAdjustmentRepository = lotAdjustmentRepository;
        _itemLotRepository = itemLotRepository;
        _employeeRepository = employeeRepository;
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(CreateLotAdjustmentCommand request, CancellationToken cancellationToken)
    {
        Item? item = await _itemRepository.GetItemById(request.ItemId, request.Unit);
        if (item is null)
        {
            throw new EntityNotFoundException("Item does not exist.");
        }
        var itemLot = await _itemLotRepository.GetLotByLotId(request.LotId);
        if (itemLot is null)
        {
            throw new EntityNotFoundException($"Itemlot {request.LotId} does not exist");
        }
        var employee = await _employeeRepository.GetEmployeeByName(request.EmployeeName);
        if (employee is null)
        {
            throw new EntityNotFoundException($"Employee {request.EmployeeName} does not exist");
        }
        
        var lotAdjustment = new LotAdjustment(request.LotId, request.OldPurchaseOrderNumber,  request.BeforeQuantity, 
            request.Unit, request.Note, DateTime.Now, item.Id, employee.Id);
        lotAdjustment.Update(request.AfterQuantity, request.NewPurchaseOrderNumber);
        _lotAdjustmentRepository.Add(lotAdjustment);

        return await _lotAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
