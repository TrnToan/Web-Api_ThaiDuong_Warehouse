using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class LotAdjustedDomainEventHandler : INotificationHandler<LotAdjustedDomainEvent>
{
    private readonly IItemLotRepository _itemLotRepository;
    public LotAdjustedDomainEventHandler(IItemLotRepository itemLotRepository)
    {
        _itemLotRepository = itemLotRepository;
    }
    public async Task Handle(LotAdjustedDomainEvent notification, CancellationToken cancellationToken)
    {
        var quantity = notification.AfterQuantity;
        var purchaseOrderNumber = notification.NewPurchaseOrderNumber;
        var itemLot = await _itemLotRepository.GetLotByLotId(notification.LotId);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        itemLot.Update(quantity, purchaseOrderNumber);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        _itemLotRepository.UpdateLot(itemLot);
    }
}
