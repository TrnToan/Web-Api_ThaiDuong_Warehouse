using ThaiDuongWarehouse.Domain.AggregateModels.ItemLotLocationAggregate;
using ThaiDuongWarehouse.Domain.DomainEvents.IsolatedItemLotEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers.IsolatedItemLotEventHandlers;

public class BackToItemLotDomainEventHandler : INotificationHandler<BackToItemLotDomainEvent>
{
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IItemLotLocationRepository _itemLotLocationRepository;

    public BackToItemLotDomainEventHandler(IItemLotRepository itemLotRepository, IItemLotLocationRepository itemLotLocationRepository)
    {
        _itemLotRepository = itemLotRepository;
        _itemLotLocationRepository = itemLotLocationRepository;
    }

    public async Task Handle(BackToItemLotDomainEvent notification, CancellationToken cancellationToken)
    {
        foreach (var unisolatedSublot in notification.UnisolatedItemLotLocations)
        {
            var itemLotLocation = await _itemLotLocationRepository.GetByIdAsync(unisolatedSublot.ItemLotId, unisolatedSublot.LocationId);

            if (itemLotLocation == null)
            {
                await _itemLotLocationRepository.AddAsync(unisolatedSublot);
            }
            else
            {
                itemLotLocation.UpdateQuantity(unisolatedSublot.QuantityPerLocation);
                _itemLotLocationRepository.Update(itemLotLocation);
            }
        }

        notification.ItemLot.Update(notification.UnisolatedQuantity);
        _itemLotRepository.UpdateLot(notification.ItemLot);
    }
}
