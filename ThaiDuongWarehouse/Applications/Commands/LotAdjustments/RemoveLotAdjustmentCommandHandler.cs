namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

public class RemoveLotAdjustmentCommandHandler : IRequestHandler<RemoveLotAdjustmentCommand, bool>
{
    private readonly ILotAdjustmentRepository _lotAdjustmentRepository;
    public RemoveLotAdjustmentCommandHandler(ILotAdjustmentRepository lotAdjustmentRepository)
    {
        _lotAdjustmentRepository = lotAdjustmentRepository;
    }

    public async Task<bool> Handle(RemoveLotAdjustmentCommand request, CancellationToken cancellationToken)
    {
        var lotAdjustment = await _lotAdjustmentRepository.GetAdjustmentByLotId(request.LotId);

        if (lotAdjustment is null)
        {
            throw new EntityNotFoundException($"LotAdjustment with itemlot {request.LotId} doesn't exist.");
        }

        _lotAdjustmentRepository.RemoveAdjustment(lotAdjustment);

        return await _lotAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
