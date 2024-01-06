using ThaiDuongWarehouse.Domain.AggregateModels.IsolatedItemLotAggregate;
using ThaiDuongWarehouse.Domain.DomainEvents.ItemLotEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers.ItemLotEventHandlers;

public class IsolateItemLotsDomainEventHandler : INotificationHandler<IsolateItemLotsDomainEvent>
{
    private readonly IIsolatedItemLotRepository _isolatedItemLotRepository;

    public IsolateItemLotsDomainEventHandler(IIsolatedItemLotRepository isolatedItemLotRepository)
    {
        _isolatedItemLotRepository = isolatedItemLotRepository;
    }

    public async Task Handle(IsolateItemLotsDomainEvent notification, CancellationToken cancellationToken)
    {
        var isolatedItemLot = await _isolatedItemLotRepository.GetAsync(notification.ItemLotId);
        if (isolatedItemLot is null)
        {
            isolatedItemLot = new IsolatedItemLot(notification.ItemLotId, notification.Quantity, notification.ProductionDate,
                notification.ExpirationDate, notification.ItemId);

            await _isolatedItemLotRepository.AddAsync(isolatedItemLot);
            return;
        }

        isolatedItemLot.UpdateQuantity(notification.Quantity);
        _isolatedItemLotRepository.Update(isolatedItemLot);
    }
}
