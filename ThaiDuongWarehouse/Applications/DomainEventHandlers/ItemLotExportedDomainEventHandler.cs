using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class ItemLotExportedDomainEventHandler : INotificationHandler<ItemLotExportedDomainEvent>
{
    private readonly IItemLotRepository _itemLotRepository;
    public ItemLotExportedDomainEventHandler(IItemLotRepository itemLotRepository)
    {
        _itemLotRepository = itemLotRepository;
    }

    public Task Handle(ItemLotExportedDomainEvent notification, CancellationToken cancellationToken)
    {
        _itemLotRepository.RemoveLots(notification.ItemLots);
        return Task.CompletedTask;
    }
}
