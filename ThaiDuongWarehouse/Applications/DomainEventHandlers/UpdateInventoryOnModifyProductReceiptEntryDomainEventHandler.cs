using ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductInventoryAggregate;
using ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class UpdateInventoryOnModifyProductReceiptEntryDomainEventHandler : INotificationHandler<UpdateInventoryOnModifyProductReceiptEntryDomainEvent>
{
    private readonly IFinishedProductInventoryRepository _finishedProductInventoryRepository;
    private bool flag = false;

    public UpdateInventoryOnModifyProductReceiptEntryDomainEventHandler(IFinishedProductInventoryRepository finishedProductInventoryRepository)
    {
        _finishedProductInventoryRepository = finishedProductInventoryRepository;
    }

    public async Task Handle(UpdateInventoryOnModifyProductReceiptEntryDomainEvent notification, CancellationToken cancellationToken)
    {
        var oldProductEntry = await _finishedProductInventoryRepository.GetFinishedProductInventory(notification.Item.ItemId,
                notification.Item.Unit, notification.OldPurchaseOrderNumber);

        var newProductEntry = await _finishedProductInventoryRepository.GetFinishedProductInventory(notification.Item.ItemId,
            notification.Item.Unit, notification.NewPurchaseOrderNumber);

        if (oldProductEntry is null)
        {
            throw new EntityNotFoundException($"Not found product entry with {notification.Item.ItemId} & {notification.OldPurchaseOrderNumber}");
        }

        if (notification.NewPurchaseOrderNumber != null && notification.NewPurchaseOrderNumber != notification.OldPurchaseOrderNumber)
        {          
            if (newProductEntry is null)
            {
                FinishedProductInventory productInventory = new FinishedProductInventory(notification.NewPurchaseOrderNumber,
                    notification.NewQuantity, notification.Item);

                oldProductEntry.UpdateQuantity(-notification.OldQuantity);

                await _finishedProductInventoryRepository.Add(productInventory);
                _finishedProductInventoryRepository.Update(oldProductEntry);
            }
            else
            {
                oldProductEntry.UpdateQuantity(-notification.OldQuantity);
                newProductEntry.UpdateQuantity(notification.OldQuantity);

                _finishedProductInventoryRepository.Update(oldProductEntry);
                _finishedProductInventoryRepository.Update(newProductEntry);
            }
            flag = true;
        }

        if (notification.NewQuantity != notification.OldQuantity && newProductEntry is not null)
        {
            double changedQuantity = notification.NewQuantity - notification.OldQuantity;
            if (flag)
            {
                newProductEntry.UpdateQuantity(changedQuantity);
                _finishedProductInventoryRepository.Update(newProductEntry);
            }                
            else
            {
                oldProductEntry.UpdateQuantity(changedQuantity);
                _finishedProductInventoryRepository.Update(oldProductEntry);
            }
        }
        await Task.CompletedTask;
    }
}
