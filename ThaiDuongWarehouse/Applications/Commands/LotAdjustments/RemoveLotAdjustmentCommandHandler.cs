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
#pragma warning disable CS8604 // Possible null reference argument.
        _lotAdjustmentRepository.RemoveAdjustment(lotAdjustment);
#pragma warning restore CS8604 // Possible null reference argument.

        return await _lotAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
