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
        var item = await _itemRepository.GetItemById(notification.ItemId);
        var changedQuantity = notification.AfterQuantity - notification.BeforeQuantity;

#pragma warning disable CS8604 // Possible null reference argument.
        var inventoryLogEntry = new InventoryLogEntry(notification.Timestamp, notification.LotId, notification.BeforeQuantity,
            changedQuantity, item);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        itemLot.Update(notification.AfterQuantity, notification.NewPurchaseOrderNumber);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        _itemLotRepository.UpdateLot(itemLot);
        _inventoryLogEntryRepository.Add(inventoryLogEntry);
    }
}
