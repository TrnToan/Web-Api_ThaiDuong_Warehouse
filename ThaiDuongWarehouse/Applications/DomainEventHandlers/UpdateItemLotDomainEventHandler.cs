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
        var itemLot = await _itemLotRepository.GetLotByLotId(notification.OldItemLotId);     

        if (itemLot is null)
        {
            throw new EntityNotFoundException($"Itemlot with Id {notification.OldItemLotId} doesn't exist.");
        }
        else
        {
            string lotId = notification.NewItemLotId ?? notification.OldItemLotId;
            itemLot.UpdateExistedLot(lotId, notification.ItemLotLocations, notification.Quantity,
                notification.ProductionDate, notification.ExpirationDate);

            _itemLotRepository.UpdateLot(itemLot);
        }
    }
}
