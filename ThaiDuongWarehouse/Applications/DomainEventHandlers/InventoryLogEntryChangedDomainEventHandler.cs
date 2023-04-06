using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;
using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class InventoryLogEntryChangedDomainEventHandler : INotificationHandler<InventoryLogEntryChangedDomainEvent>
{
    private readonly IInventoryLogEntryRepository _inventoryLogEntryRepository;
    private readonly IItemLotRepository _itemLotRepository;
    public InventoryLogEntryChangedDomainEventHandler(IInventoryLogEntryRepository inventoryLogEntryRepository, 
        IItemLotRepository itemLotRepository)
    {
        _inventoryLogEntryRepository = inventoryLogEntryRepository;
        _itemLotRepository = itemLotRepository;
    }

    public async Task Handle(InventoryLogEntryChangedDomainEvent notification, CancellationToken cancellationToken)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        InventoryLogEntry latestEntry = await _inventoryLogEntryRepository.GetLatestLogEntry(notification.Item.Id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        double tempQuantity = 0;
        if (latestEntry is null)
        {
            IEnumerable<ItemLot> itemLots = await _itemLotRepository.GetLotsByItemId(notification.Item.ItemId, notification.Item.Unit);
            tempQuantity = itemLots.Sum(x => x.Quantity);
        }
        else
            tempQuantity = latestEntry.BeforeQuantity + latestEntry.ChangedQuantity;

        InventoryLogEntry newEntry = new (notification.Item.Id, notification.ItemLotId, notification.Timestamp,
            tempQuantity, notification.Quantity, notification.Item.Unit);

        _inventoryLogEntryRepository.Add(newEntry);
    }
}
