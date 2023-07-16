using ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductInventoryAggregate;
using ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate;
using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class UpdateInventoryOnCreateProductReceiptDomainEventHandler : INotificationHandler<UpdateInventoryOnCreateProductReceiptDomainEvent>
{
    private readonly IFinishedProductInventoryRepository _finisedProductInventoryRepository;

    public UpdateInventoryOnCreateProductReceiptDomainEventHandler(IFinishedProductInventoryRepository finisedProductInventoryRepository)
    {
        _finisedProductInventoryRepository = finisedProductInventoryRepository;
    }

    public async Task Handle(UpdateInventoryOnCreateProductReceiptDomainEvent notification, CancellationToken cancellationToken)
    {
        var productInventory = new FinishedProductInventory(notification.PurchaseOrderNumber, notification.Quantity,
            notification.Timestamp, notification.Item);

        await _finisedProductInventoryRepository.Add(productInventory);
    }
}
