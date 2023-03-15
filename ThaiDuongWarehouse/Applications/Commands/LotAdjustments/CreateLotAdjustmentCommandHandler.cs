namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

public class CreateLotAdjustmentCommandHandler : IRequestHandler<CreateLotAdjustmentCommand, bool>
{
    private readonly ILotAdjustmentRepository _lotAdjustmentRepository;
    public CreateLotAdjustmentCommandHandler(ILotAdjustmentRepository lotAdjustmentRepository)
    {
        _lotAdjustmentRepository = lotAdjustmentRepository;
    }

    public async Task<bool> Handle(CreateLotAdjustmentCommand request, CancellationToken cancellationToken)
    {
        var adjustment = new LotAdjustment(request.LotId, request.NewPurchaseOrderNumber,
            request.OldPurchaseOrderNumber, request.Note, request.BeforeQuantity, request.AfterQuantity,
            request.Timestamp, request.Employee.Id, request.Item.Id);

        _lotAdjustmentRepository.Add(adjustment);
        return await _lotAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
