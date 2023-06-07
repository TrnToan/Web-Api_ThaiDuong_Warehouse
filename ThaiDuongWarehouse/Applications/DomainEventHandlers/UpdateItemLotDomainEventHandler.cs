using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class UpdateItemLotDomainEventHandler : INotificationHandler<UpdateItemLotDomainEvent>
{
    private readonly IItemLotRepository _itemLotRepository;

    public UpdateItemLotDomainEventHandler(IItemLotRepository itemLotRepository)
    {
        _itemLotRepository = itemLotRepository;
    }

    public async Task Handle(UpdateItemLotDomainEvent notification, CancellationToken cancellationToken)
    {
        var itemLot = await _itemLotRepository.GetLotByLotId(notification.ItemLotId);     

        if (itemLot is null)
        {
            var newLot = new ItemLot(notification.ItemLotId, notification.LocationId, notification.ItemId, notification.Quantity,
               notification.Unit, notification.SublotSize, notification.SublotUnit, notification.PurchaseOrderNumber,
               notification.ProductionDate, notification.ExpirationDate);

            _itemLotRepository.AddLot(newLot);
        }
        else
        {
            itemLot.UpdateConfirmedLot(notification.LocationId, notification.Quantity, notification.PurchaseOrderNumber,
                notification.ProductionDate, notification.ExpirationDate);

            _itemLotRepository.UpdateLot(itemLot);
        }
    }
}
