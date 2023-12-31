using ThaiDuongWarehouse.Domain.DomainEvents.GoodsIssueEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers.GoodsIssueEventHandlers;

public class ItemLotsExportedDomainEventHandler : INotificationHandler<ItemLotsExportedDomainEvent>
{
    private readonly IItemLotRepository _itemLotRepository;
    public ItemLotsExportedDomainEventHandler(IItemLotRepository itemLotRepository)
    {
        _itemLotRepository = itemLotRepository;
    }

    public Task Handle(ItemLotsExportedDomainEvent notification, CancellationToken cancellationToken)
    {
        _itemLotRepository.RemoveLots(notification.ItemLots);
        return Task.CompletedTask;
    }
}
