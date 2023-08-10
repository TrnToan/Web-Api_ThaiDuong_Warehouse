using ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class UpdateInventoryOnRemoveProductIssueEntryDomainEventHandler : INotificationHandler<UpdateInventoryOnRemoveProductIssueEntryDomainEvent>
{
    private readonly IFinishedProductInventoryRepository _finishedProductInventoryRepository;

    public UpdateInventoryOnRemoveProductIssueEntryDomainEventHandler(IFinishedProductInventoryRepository finishedProductInventoryRepository)
    {
        _finishedProductInventoryRepository = finishedProductInventoryRepository;
    }

    public async Task Handle(UpdateInventoryOnRemoveProductIssueEntryDomainEvent notification, CancellationToken cancellationToken)
    {
        var productInventory = await _finishedProductInventoryRepository.GetFinishedProductInventory(notification.Item.ItemId,
            notification.Item.Unit, notification.PurchaseOrderNumber);
        if (productInventory is null)
        {
            var restoreProductInventory = new FinishedProductInventory(notification.PurchaseOrderNumber, notification.Quantity, 
                notification.Item);

            await _finishedProductInventoryRepository.Add(restoreProductInventory);
        }
        else
        {
            productInventory.UpdateQuantity(notification.Quantity);
            _finishedProductInventoryRepository.Update(productInventory);
        }
        await Task.CompletedTask; 
    }
}
