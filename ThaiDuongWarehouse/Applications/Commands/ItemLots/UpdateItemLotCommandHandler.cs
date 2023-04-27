namespace ThaiDuongWarehouse.Api.Applications.Commands.ItemLots;

public class UpdateItemLotCommandHandler : IRequestHandler<UpdateItemLotCommand, bool>
{
    private readonly IItemLotRepository _itemLotRepository;

    public UpdateItemLotCommandHandler(IItemLotRepository itemLotRepository)
    {
        _itemLotRepository = itemLotRepository;
    }

    public async Task<bool> Handle(UpdateItemLotCommand request, CancellationToken cancellationToken)
    {
        var itemLot = await _itemLotRepository.GetLotByLotId(request.ItemLotId);
        if (itemLot is null)
        {
            throw new EntityNotFoundException($"Itemlot with Id {request.ItemLotId} doesn't exist.");
        }

        itemLot.UpdateState(request.IsIsolated);
        return await _itemLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
