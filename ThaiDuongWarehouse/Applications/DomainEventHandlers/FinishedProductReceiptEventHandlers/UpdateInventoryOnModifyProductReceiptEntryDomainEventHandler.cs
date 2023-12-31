using ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate;
using ThaiDuongWarehouse.Domain.DomainEvents.FinishedProductReceiptEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers.FinishedProductReceiptEventHandlers;

public class UpdateInventoryOnModifyProductReceiptEntryDomainEventHandler : INotificationHandler<UpdateInventoryOnModifyProductReceiptEntryDomainEvent>
{
    private readonly IFinishedProductInventoryRepository _finishedProductInventoryRepository;
    private readonly FinishedProductInventoryService _service;
    private bool poFlag = false;

    public UpdateInventoryOnModifyProductReceiptEntryDomainEventHandler(IFinishedProductInventoryRepository finishedProductInventoryRepository,
        FinishedProductInventoryService finishedProductInventoryService)
    {
        _finishedProductInventoryRepository = finishedProductInventoryRepository;
        _service = finishedProductInventoryService;
    }

    public async Task Handle(UpdateInventoryOnModifyProductReceiptEntryDomainEvent notification, CancellationToken cancellationToken)
    {
        bool existedProductEntryFlag = false;
        var oldProductEntry = await _finishedProductInventoryRepository.GetFinishedProductInventory(notification.Item.ItemId,
                notification.Item.Unit, notification.OldPurchaseOrderNumber);

        var newProductEntry = await _finishedProductInventoryRepository.GetFinishedProductInventory(notification.Item.ItemId,
            notification.Item.Unit, notification.NewPurchaseOrderNumber);

        if (oldProductEntry is null)
        {
            throw new EntityNotFoundException(nameof(FinishedProductReceiptEntry), notification.Item.ItemId + " with PO: " + notification.OldPurchaseOrderNumber);
        }

        if (notification.NewPurchaseOrderNumber != null && notification.NewPurchaseOrderNumber != notification.OldPurchaseOrderNumber)
        {
            oldProductEntry.UpdateQuantity(-notification.OldQuantity);

            if (oldProductEntry.Quantity > 0)
                _finishedProductInventoryRepository.Update(oldProductEntry);
            else
                _finishedProductInventoryRepository.Remove(oldProductEntry);

            if (newProductEntry is null)
            {
                newProductEntry = _service.GetInventory(notification.Item.ItemId, notification.Item.Unit,
                    notification.NewPurchaseOrderNumber);

                if (newProductEntry is null)
                {
                    FinishedProductInventory productInventory = new(notification.NewPurchaseOrderNumber,
                    notification.NewQuantity, notification.Item);

                    await _finishedProductInventoryRepository.Add(productInventory);
                    _service.Add(productInventory);
                }
                else
                {
                    newProductEntry.UpdateQuantity(notification.OldQuantity);
                    existedProductEntryFlag = true;
                }
            }
            else
            {
                newProductEntry.UpdateQuantity(notification.OldQuantity);
                _finishedProductInventoryRepository.Update(newProductEntry);
            }
            poFlag = true;
        }

        if (notification.NewQuantity != notification.OldQuantity && newProductEntry is not null)
        {
            double changedQuantity = notification.NewQuantity - notification.OldQuantity;
            if (poFlag)
            {
                newProductEntry.UpdateQuantity(changedQuantity);
                if (!existedProductEntryFlag)
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
