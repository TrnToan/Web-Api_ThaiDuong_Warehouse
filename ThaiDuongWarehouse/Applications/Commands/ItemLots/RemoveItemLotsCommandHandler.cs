using ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Commands.ItemLots;

public class RemoveItemLotsCommandHandler : IRequestHandler<RemoveItemLotsCommand, bool>
{
    private readonly IItemLotRepository _itemLotRepository;

    public RemoveItemLotsCommandHandler(IItemLotRepository itemLotRepository)
    {
        _itemLotRepository = itemLotRepository;
    }

    public async Task<bool> Handle(RemoveItemLotsCommand request, CancellationToken cancellationToken)
    {      
        var lot = await _itemLotRepository.GetLotByLotId(request.ItemLotId);
        if (lot is null) 
        {
            throw new EntityNotFoundException($"ItemLot with Id {request.ItemLotId} doesn't exist.");
        }
            
        if (!lot.IsIsolated)
        {
            throw new EntityNotFoundException("It is not allowed to delete unisolated lot.");
        }           
        ItemLot.Reject(lot);
        _itemLotRepository.RemoveLot(lot);
        return await _itemLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
