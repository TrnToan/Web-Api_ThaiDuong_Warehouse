using ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductInventoryAggregate;
using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class UpdateFinishedProductInventoryDomainEventHandler : INotificationHandler<UpdateFinishedProductInventoryDomainEvent>
{
    private readonly IFinishedProductInventoryRepository _finishedProductInventoryRepository;

    public UpdateFinishedProductInventoryDomainEventHandler(IFinishedProductInventoryRepository finishedProductInventoryRepository)
    {
        _finishedProductInventoryRepository = finishedProductInventoryRepository;
    }

    public async Task Handle(UpdateFinishedProductInventoryDomainEvent notification, CancellationToken cancellationToken)
    {
        var productInventory = await _finishedProductInventoryRepository
            .GetFinishedProductInventory(notification.Item.ItemId, notification.Item.Unit, notification.OldPurchaseOrderNumber, notification.Timestamp);

        if (productInventory is null)
        {
            throw new EntityNotFoundException($"FinishedProductInventory, {notification.Item.ItemId} & {notification.OldPurchaseOrderNumber}");
        }
        productInventory.UpdateProductInventory(notification.NewPurchaseOrderNumber, notification.Quantity);

        _finishedProductInventoryRepository.Update(productInventory);
    }
}
