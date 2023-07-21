using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class LotAdjustedDomainEventHandler : INotificationHandler<LotAdjustedDomainEvent>
{
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IInventoryLogEntryRepository _inventoryLogEntryRepository;
    public LotAdjustedDomainEventHandler(IItemLotRepository itemLotRepository, IItemRepository itemRepository, 
        IInventoryLogEntryRepository inventoryLogEntryRepository)
    {
        _itemLotRepository = itemLotRepository;
        _itemRepository = itemRepository;
        _inventoryLogEntryRepository = inventoryLogEntryRepository;
    }
    public async Task Handle(LotAdjustedDomainEvent notification, CancellationToken cancellationToken)
    {
        var itemLot = await _itemLotRepository.GetLotByLotId(notification.LotId);
        if (itemLot is null)
            throw new EntityNotFoundException(notification.LotId);

        var itemLots = await _itemLotRepository.GetLotsByItemId(notification.ItemId, notification.Unit);
        double beforeQuantity = itemLots.Sum(x => x.Quantity);

        var item = await _itemRepository.GetItemById(notification.ItemId, notification.Unit);
        if (item is null)
        {
            throw new EntityNotFoundException($"Item, {notification.ItemId} & {notification.Unit}");
        }
        double changedQuantity = notification.AfterQuantity - notification.BeforeQuantity;

        var inventoryLogEntry = new InventoryLogEntry(item.Id, notification.LotId, notification.Timestamp, beforeQuantity, 
            changedQuantity, 0, -changedQuantity);

        itemLot.Update(notification.AfterQuantity);

        _itemLotRepository.UpdateLot(itemLot);
        _inventoryLogEntryRepository.Add(inventoryLogEntry);
    }
}
