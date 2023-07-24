namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class LotAdjustedDomainEventHandler : INotificationHandler<LotAdjustedDomainEvent>
{
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IInventoryLogEntryRepository _inventoryLogEntryRepository;
    private readonly IStorageRepository _storageRepository;
    public LotAdjustedDomainEventHandler(IItemLotRepository itemLotRepository, IItemRepository itemRepository, 
        IInventoryLogEntryRepository inventoryLogEntryRepository, IStorageRepository storageRepository)
    {
        _itemLotRepository = itemLotRepository;
        _itemRepository = itemRepository;
        _inventoryLogEntryRepository = inventoryLogEntryRepository;
        _storageRepository = storageRepository;
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
        
        foreach (var sublot in notification.SublotAdjustments)
        {
            var location = await _storageRepository.GetLocationById(sublot.LocationId);
            if (location is null)
            {
                throw new EntityNotFoundException($"Location with Id {sublot.LocationId} not found.");
            }
            itemLot.UpdateItemLotLocation(itemLot.Id, location.Id, sublot.AfterQuantityPerLocation);
        }

        itemLot.Update(notification.AfterQuantity);

        _itemLotRepository.UpdateLot(itemLot);
        _inventoryLogEntryRepository.Add(inventoryLogEntry);
    }
}
