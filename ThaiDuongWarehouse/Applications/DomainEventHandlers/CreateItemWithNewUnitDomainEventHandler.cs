using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class CreateItemWithNewUnitDomainEventHandler : INotificationHandler<CreateItemWithNewUnitDomainEvent>
{
    private readonly IItemRepository _itemRepository;
    public CreateItemWithNewUnitDomainEventHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public Task Handle(CreateItemWithNewUnitDomainEvent notification, CancellationToken cancellationToken)
    {
        Item item = new (notification.ItemId, notification.ItemClassId, notification.Unit);

        _itemRepository.Add(item);
        return Task.CompletedTask;
    }
}
