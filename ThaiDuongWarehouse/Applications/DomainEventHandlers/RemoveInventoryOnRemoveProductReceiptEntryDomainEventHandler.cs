using ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductInventoryAggregate;
using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class RemoveInventoryOnRemoveProductReceiptEntryDomainEventHandler : INotificationHandler<RemoveInventoryOnRemoveProductReceiptEntryDomainEvent>
{
    private readonly IFinishedProductInventoryRepository _finishedProductInventoryRepository;

    public RemoveInventoryOnRemoveProductReceiptEntryDomainEventHandler(IFinishedProductInventoryRepository finishedProductInventoryRepository)
    {
        _finishedProductInventoryRepository = finishedProductInventoryRepository;
    }

    public async Task Handle(RemoveInventoryOnRemoveProductReceiptEntryDomainEvent notification, CancellationToken cancellationToken)
    {
        var productInventory = await _finishedProductInventoryRepository.GetFinishedProductInventory(
            notification.Item.ItemId, notification.Item.Unit, notification.PurchaseOrderNumber, notification.Timestamp);

        if (productInventory is null) 
        {
            throw new EntityNotFoundException($"FinishedProductInventory, {notification.Item.ItemName} & {notification.PurchaseOrderNumber}");
        }

        _finishedProductInventoryRepository.Remove(productInventory);
    }
}
