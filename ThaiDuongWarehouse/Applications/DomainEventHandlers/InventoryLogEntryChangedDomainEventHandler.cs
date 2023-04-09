using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;
using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class InventoryLogEntryChangedDomainEventHandler : INotificationHandler<InventoryLogEntryChangedDomainEvent>
{
    private readonly IInventoryLogEntryRepository _inventoryLogEntryRepository;
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IItemRepository _itemRepository;
    public InventoryLogEntryChangedDomainEventHandler(IInventoryLogEntryRepository inventoryLogEntryRepository, 
        IItemLotRepository itemLotRepository, IItemRepository itemRepository)
    {
        _inventoryLogEntryRepository = inventoryLogEntryRepository;
        _itemLotRepository = itemLotRepository;
        _itemRepository = itemRepository;
    }

    public async Task Handle(InventoryLogEntryChangedDomainEvent notification, CancellationToken cancellationToken)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        Item item = await _itemRepository.GetItemByEntityId(notification.ItemId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        InventoryLogEntry? latestEntry = await _inventoryLogEntryRepository.GetLatestLogEntry(notification.ItemId);
        double tempQuantity = 0;

        if (latestEntry is null)
        {
            IEnumerable<ItemLot> itemLots = await _itemLotRepository.GetLotsByItemId(item.ItemId, item.Unit);
            List<ItemLot> unIsolatedItemLots = itemLots.Where(lot => lot.IsIsolated == false).ToList();
            tempQuantity = unIsolatedItemLots.Sum(x => x.Quantity);
        }
        else
            tempQuantity = latestEntry.BeforeQuantity + latestEntry.ChangedQuantity;

        InventoryLogEntry newEntry = new (notification.ItemId, notification.ItemLotId, notification.Timestamp,
            tempQuantity, notification.Quantity, item.Unit);

        _inventoryLogEntryRepository.Add(newEntry);
    }
}
