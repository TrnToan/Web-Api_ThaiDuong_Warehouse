using ThaiDuongWarehouse.Domain.DomainEvents.FinishedProductIssueEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers.FinishedProductIssueEventHandlers;

public class UpdateInventoryOnCreateProductIssueDomainEventHandler : INotificationHandler<UpdateInventoryOnCreateProductIssueDomainEvent>
{
    private readonly IFinishedProductInventoryRepository _finishedProductInventoryRepository;

    public UpdateInventoryOnCreateProductIssueDomainEventHandler(IFinishedProductInventoryRepository finishedProductInventoryRepository)
    {
        _finishedProductInventoryRepository = finishedProductInventoryRepository;
    }

    public async Task Handle(UpdateInventoryOnCreateProductIssueDomainEvent notification, CancellationToken cancellationToken)
    {
        var productInventory = await _finishedProductInventoryRepository.GetFinishedProductInventory(
            notification.Item.ItemId, notification.Item.Unit, notification.PurchaseOrderNumber);

        if (productInventory is null)
        {
            throw new EntityNotFoundException($"Product with item {notification.Item.ItemId} & {notification.PurchaseOrderNumber} does not exist.");
        }

        double productInventoryQuantity = productInventory.Quantity;

        productInventory.UpdateQuantity(-notification.Quantity);
        if (productInventory.Quantity == 0)
        {
            _finishedProductInventoryRepository.Remove(productInventory);
        }
        else if (productInventory.Quantity < 0)
            throw new InvalidProductIssueQuantityException(notification.Item.ItemId, notification.Item.Unit,
                notification.PurchaseOrderNumber, productInventoryQuantity, notification.Quantity);

        else
            _finishedProductInventoryRepository.Update(productInventory);
    }
}
