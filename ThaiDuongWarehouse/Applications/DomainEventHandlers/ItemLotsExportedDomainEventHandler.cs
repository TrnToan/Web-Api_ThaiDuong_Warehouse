using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class ItemLotsExportedDomainEventHandler : INotificationHandler<ItemLotsExportedDomainEvent>
{
    private readonly IItemLotRepository _itemLotRepository;
    public ItemLotsExportedDomainEventHandler(IItemLotRepository itemLotRepository)
    {
        _itemLotRepository = itemLotRepository;
    }

    public Task Handle(ItemLotsExportedDomainEvent notification, CancellationToken cancellationToken)
    {
        var removedLots = notification.ItemLots;
        foreach(var lot in removedLots)
        {
            _itemLotRepository.RemoveLot(lot);
        }
        return Task.CompletedTask;
    }
}
