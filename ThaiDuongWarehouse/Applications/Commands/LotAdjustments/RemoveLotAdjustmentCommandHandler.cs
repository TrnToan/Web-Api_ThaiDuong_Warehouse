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

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        if (lotAdjustment.IsConfirmed)
        {
            throw new Exception($"Lot adjustment with Id {request.LotId} is not allowed to delete.");
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        _lotAdjustmentRepository.RemoveAdjustment(lotAdjustment);

        return await _lotAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
