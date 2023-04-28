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
        List<ItemLot> removedLots = new();
        foreach(string lotId in request.ItemLotIds)
        {
            var lot = await _itemLotRepository.GetLotByLotId(lotId);
            if (lot is null) 
            {
                throw new EntityNotFoundException($"ItemLot with Id {lotId} doesn't exist.");
            }
            
            if (lot.IsIsolated == false)
            {
                throw new EntityNotFoundException("It is not allowed to delete one of the itemlots in the list.");
            }
            removedLots.Add(lot);
            ItemLot.Reject(lot);
        }
        _itemLotRepository.RemoveLots(removedLots);
        return await _itemLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
