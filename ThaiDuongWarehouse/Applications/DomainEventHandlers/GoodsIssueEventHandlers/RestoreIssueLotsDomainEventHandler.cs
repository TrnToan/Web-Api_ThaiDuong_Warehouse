using ThaiDuongWarehouse.Domain.DomainEvents.GoodsIssueEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers.GoodsIssueEventHandlers;

public class RestoreIssueLotsDomainEventHandler : INotificationHandler<RestoreIssueLotsDomainEvent>
{
    private readonly IItemLotRepository _itemLotRepository;

    public RestoreIssueLotsDomainEventHandler(IItemLotRepository itemLotRepository)
    {
        _itemLotRepository = itemLotRepository;
    }

    public Task Handle(RestoreIssueLotsDomainEvent notification, CancellationToken cancellationToken)
    {
        _itemLotRepository.Addlots(notification.RestoredItemLots);
        _itemLotRepository.UpdateLots(notification.ExistingItemLots);
        return Task.CompletedTask;
    }
}
