namespace ThaiDuongWarehouse.Api.Applications.Commands.IsolatedItemLots;

public class UnisolateItemLotCommandHandler : IRequestHandler<UnisolateItemLotCommand, bool>
{
    private readonly IIsolatedItemLotRepository _isolatedItemLotRepository;
    private readonly IStorageRepository _storageRepository;
    private readonly IItemLotRepository _itemLotRepository;

    public UnisolateItemLotCommandHandler(IIsolatedItemLotRepository isolatedItemLotRepository, 
        IStorageRepository storageRepository, IItemLotRepository itemLotRepository)
    {
        _isolatedItemLotRepository = isolatedItemLotRepository;
        _storageRepository = storageRepository;
        _itemLotRepository = itemLotRepository;
    }

    public async Task<bool> Handle(UnisolateItemLotCommand request, CancellationToken cancellationToken)
    {
        var isolatedLot = await _isolatedItemLotRepository.GetAsync(request.ItemLotId);
        if (isolatedLot is null)
        {
            throw new EntityNotFoundException(nameof(IsolatedItemLot), request.ItemLotId);
        }

        var itemLot = await _itemLotRepository.GetLotByLotId(request.ItemLotId);

        List<ItemLotLocation> itemLotLocations = new List<ItemLotLocation>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Dereference of a possibly null reference.
        foreach (var unisolatedSublot in request.UnisolatedItemSublots)
        {
            var location = await _storageRepository.GetLocationById(unisolatedSublot.LocationId);
            if (location is null)
            {
                throw new EntityNotFoundException(nameof(Location), unisolatedSublot.LocationId);
            }

            ItemLotLocation itemLotLocation = new ItemLotLocation(itemLot.Id, location.Id, unisolatedSublot.QuantityPerLocation);
            itemLotLocations.Add(itemLotLocation);
        }
        double unisolatedQuantity = request.UnisolatedItemSublots.Sum(sublot => sublot.QuantityPerLocation);

        isolatedLot.UpdateQuantity(-unisolatedQuantity);
        if (isolatedLot.Quantity == 0)
        {
            _isolatedItemLotRepository.Remove(isolatedLot);
            itemLot.Unisolate();
        }
        else
            _isolatedItemLotRepository.Update(isolatedLot);

        isolatedLot.BackToItemLot(itemLot, itemLotLocations, unisolatedQuantity);

        return await _isolatedItemLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Dereference of a possibly null reference.