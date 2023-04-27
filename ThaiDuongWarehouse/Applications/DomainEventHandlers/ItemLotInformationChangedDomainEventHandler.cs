using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class ItemLotInformationChangedDomainEventHandler : INotificationHandler<ItemLotInformationChangedDomainEvent>
{
    private readonly IItemLotRepository _itemLotRepository;
    public ItemLotInformationChangedDomainEventHandler(IItemLotRepository itemLotRepository)
    {
        _itemLotRepository = itemLotRepository;
    }
    public async Task Handle(ItemLotInformationChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        var itemLot = await _itemLotRepository.GetLotByLotId(notification.ItemLotId);
        if (itemLot is null)
        {
            throw new EntityNotFoundException($"Itemlot with Id {notification.ItemLotId} doesn't exist.");
        }

        var newQuantity = itemLot.Quantity - notification.Quantity;
        itemLot.SetQuantity(newQuantity);
        _itemLotRepository.UpdateLot(itemLot);
    }
}
