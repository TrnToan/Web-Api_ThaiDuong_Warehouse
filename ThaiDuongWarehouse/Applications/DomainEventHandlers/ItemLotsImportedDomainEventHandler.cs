using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class ItemLotsImportedDomainEventHandler : INotificationHandler<ItemLotsImportedDomainEvent>
{
    private readonly IItemLotRepository _itemLotRepository;
    public ItemLotsImportedDomainEventHandler(IItemLotRepository itemLotRepository)
    {
        _itemLotRepository = itemLotRepository;
    }

    public Task Handle(ItemLotsImportedDomainEvent notification, CancellationToken cancellationToken)
    {
        List<ItemLot> addedLots = notification.ItemLots;
        
        _itemLotRepository.Addlots(addedLots);
        return Task.CompletedTask;
    }
}
