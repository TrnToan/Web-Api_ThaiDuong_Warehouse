using ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductInventoryAggregate;
using ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate;

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
        var existedProductInventory = await _finisedProductInventoryRepository.GetFinishedProductInventory(
            notification.Item.ItemId, notification.Item.Unit, notification.PurchaseOrderNumber);

        if (existedProductInventory is null)
        {
            var productInventory = new FinishedProductInventory (notification.PurchaseOrderNumber, notification.Quantity,
            notification.Timestamp, notification.Item);

            await _finisedProductInventoryRepository.Add(productInventory);
        }
        else
        {
            existedProductInventory.UpdateQuantity(notification.Quantity);

            _finisedProductInventoryRepository.Update(existedProductInventory);
        }

    }
}
