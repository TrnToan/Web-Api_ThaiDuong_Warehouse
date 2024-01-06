using ThaiDuongWarehouse.Domain.AggregateModels.ItemLotLocationAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Commands.ItemLots;

public class IsolateItemLotCommandHandler : IRequestHandler<IsolateItemLotCommand, bool>
{
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IStorageRepository _storageRepository;

    public IsolateItemLotCommandHandler(IItemLotRepository itemLotRepository, IStorageRepository storageRepository)
    {
        _itemLotRepository = itemLotRepository;
        _storageRepository = storageRepository;
    }

    public async Task<bool> Handle(IsolateItemLotCommand request, CancellationToken cancellationToken)
    {
        var itemLot = await _itemLotRepository.GetLotByLotId(request.ItemLotId);
        if (itemLot is null)
        {
            throw new EntityNotFoundException($"Itemlot with Id {request.ItemLotId} doesn't exist.");
        }

        double isolatedQuantity = request.IsolatedItemSublots.Sum(x => x.Quantity);
        foreach(var sublot in request.IsolatedItemSublots)
        {
            var location = await _storageRepository.GetLocationById(sublot.LocationId);
            if (location is null)
            {
                throw new EntityNotFoundException(nameof(Location), sublot.LocationId);
            }

            var isolatedSublot = itemLot.ItemLotLocations.Find(ill => ill.ItemLotId == itemLot.Id 
                && ill.LocationId == location.Id);

            if (isolatedSublot is null)
            {
                throw new EntityNotFoundException(nameof(ItemLotLocation), sublot.LocationId);
            }
            isolatedSublot.UpdateQuantity(-sublot.Quantity);
            if (isolatedSublot.QuantityPerLocation == 0)
            {
                itemLot.ItemLotLocations.Remove(isolatedSublot);
            }
        }
        itemLot.Isolate(isolatedQuantity);

        _itemLotRepository.UpdateLot(itemLot);
        return await _itemLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
