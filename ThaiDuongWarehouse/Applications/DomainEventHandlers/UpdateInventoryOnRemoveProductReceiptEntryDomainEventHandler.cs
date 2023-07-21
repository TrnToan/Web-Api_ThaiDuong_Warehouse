using ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductInventoryAggregate;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class UpdateInventoryOnRemoveProductReceiptEntryDomainEventHandler : INotificationHandler<UpdateInventoryOnRemoveProductReceiptEntryDomainEvent>
{
    private readonly IFinishedProductInventoryRepository _finishedProductInventoryRepository;

    public UpdateInventoryOnRemoveProductReceiptEntryDomainEventHandler(IFinishedProductInventoryRepository finishedProductInventoryRepository)
    {
        _finishedProductInventoryRepository = finishedProductInventoryRepository;
    }

    public async Task Handle(UpdateInventoryOnRemoveProductReceiptEntryDomainEvent notification, CancellationToken cancellationToken)
    {
        var productInventory = await _finishedProductInventoryRepository.GetFinishedProductInventory(
            notification.Item.ItemId, notification.Item.Unit, notification.PurchaseOrderNumber);

        if (productInventory is null) 
        {
            throw new EntityNotFoundException($"FinishedProductInventory, {notification.Item.ItemName} & {notification.PurchaseOrderNumber}");
        }

        productInventory.UpdateQuantity(-notification.Quantity);
        if (productInventory.Quantity > 0)
        {
            _finishedProductInventoryRepository.Update(productInventory);
        }
        else
        {
            _finishedProductInventoryRepository.Remove(productInventory);
        }
    }
}
