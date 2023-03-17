namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

public class ConfirmLotAdjustmentCommandHandler : IRequestHandler<ConfirmLotAdjustmentCommand, bool>
{
    private readonly ILotAdjustmentRepository _lotAdjustmentRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IItemRepository _itemRepository;
    public ConfirmLotAdjustmentCommandHandler(ILotAdjustmentRepository lotAdjustmentRepository, 
        IEmployeeRepository employeeRepository, IItemRepository itemRepository)
    {
        _lotAdjustmentRepository = lotAdjustmentRepository;
        _employeeRepository = employeeRepository;
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(ConfirmLotAdjustmentCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetItemById(request.ItemId);
        var employee = await _employeeRepository.GetEmployeeById(request.EmployeeId);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var lotAdjustment = new LotAdjustment(request.LotId, request.NewPurchaseOrderNumber,
            request.AfterQuantity, request.Timestamp, request.Note, item.Id, employee.Id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        lotAdjustment.Confirm(request.LotId, employee, request.Timestamp, request.AfterQuantity, request.NewPurchaseOrderNumber);
        return await _lotAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
