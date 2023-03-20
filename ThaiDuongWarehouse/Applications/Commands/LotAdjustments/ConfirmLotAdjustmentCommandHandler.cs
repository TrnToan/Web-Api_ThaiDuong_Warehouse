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
        var lotAdjustment = await _lotAdjustmentRepository.GetAdjustmentByLotId(request.LotId);
        if (lotAdjustment is null)
            throw new ArgumentNullException(nameof(lotAdjustment));
        lotAdjustment.Confirm(lotAdjustment.LotId, request.ItemId, lotAdjustment.Timestamp,
            lotAdjustment.BeforeQuantity, lotAdjustment.AfterQuantity, lotAdjustment.NewPurchaseOrderNumber);
        return await _lotAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
