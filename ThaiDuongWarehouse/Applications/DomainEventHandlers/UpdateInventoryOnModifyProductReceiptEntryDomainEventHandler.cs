using ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductInventoryAggregate;
using ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class UpdateInventoryOnModifyProductReceiptEntryDomainEventHandler : INotificationHandler<UpdateInventoryOnModifyProductReceiptEntryDomainEvent>
{
    private readonly IFinishedProductInventoryRepository _finishedProductInventoryRepository;

    public UpdateInventoryOnModifyProductReceiptEntryDomainEventHandler(IFinishedProductInventoryRepository finishedProductInventoryRepository)
    {
        _finishedProductInventoryRepository = finishedProductInventoryRepository;
    }

    public async Task Handle(UpdateInventoryOnModifyProductReceiptEntryDomainEvent notification, CancellationToken cancellationToken)
    {
        var oldProducEntrytInventory = await _finishedProductInventoryRepository
            .GetFinishedProductInventory(notification.Item.ItemId, notification.Item.Unit, notification.OldPurchaseOrderNumber);

        var newProductEntryInventory = await _finishedProductInventoryRepository
            .GetFinishedProductInventory(notification.Item.ItemId, notification.Item.Unit, notification.NewPurchaseOrderNumber);

        if (oldProducEntrytInventory is null)
        {
            throw new EntityNotFoundException($"FinishedProductInventory, {notification.Item.ItemId} & {notification.OldPurchaseOrderNumber}");
        }
        
        else
        {
            if (newProductEntryInventory is null || oldProducEntrytInventory == newProductEntryInventory)
            {
                oldProducEntrytInventory.SetQuantity(notification.Quantity);

                _finishedProductInventoryRepository.Update(oldProducEntrytInventory);
            }
            else
            {
                oldProducEntrytInventory.UpdateQuantity(-notification.Quantity);
                newProductEntryInventory.UpdateQuantity(notification.Quantity);

                _finishedProductInventoryRepository.Update(oldProducEntrytInventory);
                _finishedProductInventoryRepository.Update(newProductEntryInventory);
            }            
        }
    }
}
