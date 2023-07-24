namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

public class ConfirmLotAdjustmentCommandHandler : IRequestHandler<ConfirmLotAdjustmentCommand, bool>
{
    private readonly ILotAdjustmentRepository _lotAdjustmentRepository;
    public ConfirmLotAdjustmentCommandHandler(ILotAdjustmentRepository lotAdjustmentRepository)
    {
        _lotAdjustmentRepository = lotAdjustmentRepository;
    }

    public async Task<bool> Handle(ConfirmLotAdjustmentCommand request, CancellationToken cancellationToken)
    {
        var lotAdjustment = await _lotAdjustmentRepository.GetAdjustmentByLotId(request.LotId);
        if (lotAdjustment is null)
            throw new EntityNotFoundException($"LotAdjustment with itemlot {request.LotId} doesn't exist.");

        lotAdjustment.Confirm(lotAdjustment.LotId, lotAdjustment.Item.ItemId, lotAdjustment.BeforeQuantity, lotAdjustment.AfterQuantity, 
            lotAdjustment.Item.Unit, lotAdjustment.SublotAdjustments);

        _lotAdjustmentRepository.Update(lotAdjustment);
        return await _lotAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
