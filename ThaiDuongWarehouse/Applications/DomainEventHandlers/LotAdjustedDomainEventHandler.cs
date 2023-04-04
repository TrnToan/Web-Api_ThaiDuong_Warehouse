using ThaiDuongWarehouse.Domain.AggregateModels.InventoryLogAggregate;
using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;
using ThaiDuongWarehouse.Domain.DomainEvents;

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

        var item = await _itemRepository.GetItemById(notification.ItemId, notification.Unit);
        var changedQuantity = notification.AfterQuantity - notification.BeforeQuantity;

#pragma warning disable CS8604 // Possible null reference argument.
        var inventoryLogEntry = new InventoryLogEntry(notification.Timestamp, notification.LotId, notification.BeforeQuantity,
            changedQuantity, itemLot.Unit, item);
#pragma warning restore CS8604 // Possible null reference argument.
        itemLot.Update(notification.AfterQuantity, notification.NewPurchaseOrderNumber);

        _itemLotRepository.UpdateLot(itemLot);
        _inventoryLogEntryRepository.Add(inventoryLogEntry);
    }
}
