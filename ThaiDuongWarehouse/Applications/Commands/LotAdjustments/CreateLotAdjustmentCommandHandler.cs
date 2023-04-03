
namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

public class CreateLotAdjustmentCommandHandler : IRequestHandler<CreateLotAdjustmentCommand, bool>
{
    private readonly ILotAdjustmentRepository _lotAdjustmentRepository;
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IEmployeeRepository _employeeRepository;
    public CreateLotAdjustmentCommandHandler(ILotAdjustmentRepository lotAdjustmentRepository, IItemLotRepository itemLotRepository, IEmployeeRepository employeeRepository)
    {
        _lotAdjustmentRepository = lotAdjustmentRepository;
        _itemLotRepository = itemLotRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<bool> Handle(CreateLotAdjustmentCommand request, CancellationToken cancellationToken)
    {
        var itemLot = await _itemLotRepository.GetLotByLotId(request.LotId);
        if (itemLot is null)
        {
            throw new EntityNotFoundException($"{itemLot} does not exist");
        }
        var employee = await _employeeRepository.GetEmployeeByName(request.EmployeeName);
        if (employee is null)
        {
            throw new EntityNotFoundException($"{employee} does not exist");
        }
        
        var lotAdjustment = new LotAdjustment(request.LotId, request.OldPurchaseOrderNumber,  request.BeforeQuantity, 
            request.Unit, request.Note, DateTime.Now);
        lotAdjustment.Update(request.AfterQuantity, request.NewPurchaseOrderNumber);
        _lotAdjustmentRepository.Add(lotAdjustment);

        return await _lotAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
