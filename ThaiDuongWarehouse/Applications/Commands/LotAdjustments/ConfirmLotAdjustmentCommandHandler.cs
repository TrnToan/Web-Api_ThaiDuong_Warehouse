namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

public class ConfirmLotAdjustmentCommandHandler : IRequestHandler<ConfirmLotAdjustmentCommand, bool>
{
    private readonly ILotAdjustmentRepository _lotAdjustmentRepository;
    private readonly IItemRepository _itemRepository;
    public ConfirmLotAdjustmentCommandHandler(ILotAdjustmentRepository lotAdjustmentRepository, 
        IItemRepository itemRepository)
    {
        _lotAdjustmentRepository = lotAdjustmentRepository;
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(ConfirmLotAdjustmentCommand request, CancellationToken cancellationToken)
    {
        var lotAdjustment = await _lotAdjustmentRepository.GetAdjustmentByLotId(request.LotId);
        if (lotAdjustment is null)
            throw new EntityNotFoundException($"LotAdjustment with itemlot {request.LotId} doesn't exist.");

        var item = await _itemRepository.GetItemByEntityId(lotAdjustment.ItemId);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        lotAdjustment.Confirm(lotAdjustment.LotId, item.ItemId, lotAdjustment.Unit,
            lotAdjustment.BeforeQuantity, lotAdjustment.AfterQuantity, lotAdjustment.NewPurchaseOrderNumber);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        _lotAdjustmentRepository.Update(lotAdjustment);
        return await _lotAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
