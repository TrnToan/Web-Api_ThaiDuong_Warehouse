using ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductInventoryAggregate;
using ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate;
using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class AddFinishedProductInventoryDomainEventHandler : INotificationHandler<AddFinishedProductInventoryDomainEvent>
{
    private readonly IFinishedProductInventoryRepository _finisedProductInventoryRepository;

    public AddFinishedProductInventoryDomainEventHandler(IFinishedProductInventoryRepository finisedProductInventoryRepository)
    {
        _finisedProductInventoryRepository = finisedProductInventoryRepository;
    }

    public async Task Handle(AddFinishedProductInventoryDomainEvent notification, CancellationToken cancellationToken)
    {
        var productInventory = new FinishedProductInventory(notification.PurchaseOrderNumber, notification.Quantity,
            notification.Timestamp, notification.Item);

        await _finisedProductInventoryRepository.Add(productInventory);
    }
}
