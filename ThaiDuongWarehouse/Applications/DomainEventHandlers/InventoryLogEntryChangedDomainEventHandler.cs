using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;
using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class InventoryLogEntryChangedDomainEventHandler : INotificationHandler<InventoryLogEntryChangedDomainEvent>
{
    private readonly IInventoryLogEntryRepository _inventoryLogEntryRepository;
    public InventoryLogEntryChangedDomainEventHandler(IInventoryLogEntryRepository inventoryLogEntryRepository)
    {
        _inventoryLogEntryRepository = inventoryLogEntryRepository;
    }

    public async Task Handle(InventoryLogEntryChangedDomainEvent notification, CancellationToken cancellationToken)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        InventoryLogEntry latestEntry = await _inventoryLogEntryRepository.GetLatestLogEntry(notification.Item.Id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        if (latestEntry is null)
        {
            return;
        }
        double tempQuantity = latestEntry.BeforeQuantity + latestEntry.ChangedQuantity;
        InventoryLogEntry newEntry = new InventoryLogEntry(notification.Item.Id, notification.ItemLotId, notification.Timestamp,
            tempQuantity, notification.Quantity, notification.Item.Unit);

        _inventoryLogEntryRepository.Add(newEntry);
    }
}
