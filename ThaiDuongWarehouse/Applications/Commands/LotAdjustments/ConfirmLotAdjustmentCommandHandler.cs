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

        var item = await _itemRepository.GetItemByEntityId(lotAdjustment.ItemId);
        lotAdjustment.Confirm(lotAdjustment.LotId, item.ItemId, lotAdjustment.Unit, lotAdjustment.Timestamp,
            lotAdjustment.BeforeQuantity, lotAdjustment.AfterQuantity, lotAdjustment.NewPurchaseOrderNumber);
        
        _lotAdjustmentRepository.Update(lotAdjustment);
        return await _lotAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
