using ThaiDuongWarehouse.Domain.AggregateModels;
using ThaiDuongWarehouse.Domain.DomainEvents.GoodsIssueEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers.GoodsIssueEventHandlers;

public class ItemLotInformationChangedDomainEventHandler : INotificationHandler<ItemLotInformationChangedDomainEvent>
{
    private readonly IStorageRepository _storageRepository;
    private readonly ItemLotLocationRepository _itemLotLocationRepository;
    private readonly IItemLotRepository _itemLotRepository;
    public ItemLotInformationChangedDomainEventHandler(ItemLotLocationRepository itemLotLocationRepository,
        IStorageRepository storageRepository, IItemLotRepository itemLotRepository)
    {
        _itemLotLocationRepository = itemLotLocationRepository;
        _storageRepository = storageRepository;
        _itemLotRepository = itemLotRepository;
    }
    public async Task Handle(ItemLotInformationChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        var itemLot = notification.ItemLot;
        double newLotQuantity = itemLot.Quantity - notification.GoodsIssueLot.Quantity;
        itemLot.SetQuantity(newLotQuantity);

        foreach (var goodsIssueSublot in notification.GoodsIssueLot.Sublots)
        {
            Location? location = await _storageRepository.GetLocationById(goodsIssueSublot.LocationId);
            if (location is null)
            {
                throw new EntityNotFoundException($"LocationId {goodsIssueSublot.LocationId} not found.");
            }

            var subItemLot = itemLot.ItemLotLocations?.Find(il => il.LocationId == location.Id);
            if (subItemLot == null)
            {
                throw new EntityNotFoundException($"Itemlot with locationId {location.LocationId} not found.");
            }

            double locationQuantity = subItemLot.QuantityPerLocation - goodsIssueSublot.QuantityPerLocation;
            ItemLotLocation? itemLotLocation = await _itemLotLocationRepository.GetById(subItemLot.ItemLotId, subItemLot.LocationId);
            if (itemLotLocation is null)
            {
                throw new EntityNotFoundException($"SubItemLot not found, {goodsIssueSublot.LocationId} & {itemLot.LotId}");
            }

            if (locationQuantity > 0)
            {
                itemLot.UpdateItemLotLocation(itemLotLocation.ItemLotId, itemLotLocation.LocationId, locationQuantity);
            }
            else if (locationQuantity == 0)
            {
                itemLot.RemoveItemLotLocation(itemLotLocation);
            }
            else
                throw new InvalidSublotQuantityException($"GoodsIssuelot's sublotSize {goodsIssueSublot.QuantityPerLocation} " +
                    $"larger than itemlot's sublotsize {subItemLot.QuantityPerLocation}");
        }

        _itemLotRepository.UpdateLot(itemLot);
    }
}