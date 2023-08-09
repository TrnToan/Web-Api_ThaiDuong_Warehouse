using ThaiDuongWarehouse.Api.Applications.DomainEventHandlers.DataTransactionServices;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class DeleteInventoryLogEntryDomainEventHandler : INotificationHandler<DeleteInventoryLogEntryDomainEvent>
{
    private readonly IInventoryLogEntryRepository _inventoryLogEntryRepository;
    private readonly InventoryLogEntryService _service;

    public DeleteInventoryLogEntryDomainEventHandler(IInventoryLogEntryRepository inventoryLogEntryRepository, 
        InventoryLogEntryService inventoryLogEntryService)
    {
        _inventoryLogEntryRepository = inventoryLogEntryRepository;
        _service = inventoryLogEntryService;
    }

    public async Task Handle(DeleteInventoryLogEntryDomainEvent notification, CancellationToken cancellationToken)
    {
        InventoryLogEntry? removedLogEntry;
        List<InventoryLogEntry> fixedLogEntries = new ();

        var serviceLogEntry = _service.GetLogEntry(notification.ItemId, notification.ItemLotId, notification.Timestamp);
        if (serviceLogEntry is null)
        {
            removedLogEntry = await _inventoryLogEntryRepository.GetLogEntry(notification.ItemId, notification.ItemLotId, 
                notification.Timestamp);
            if (removedLogEntry is null)
            {
                throw new EntityNotFoundException($"InventoryLogEntry of {notification} with {notification.Timestamp} not found.");
            }
            var previousLogEntry = await _inventoryLogEntryRepository.GetPreviousLogEntry(removedLogEntry.ItemId, removedLogEntry.TrackingTime);
            
            if (previousLogEntry is not null)
            {
                var preFixedLogEntries = await _inventoryLogEntryRepository.GetLatestLogEntries(previousLogEntry.ItemId, previousLogEntry.TrackingTime);
                preFixedLogEntries.Remove(removedLogEntry);

                fixedLogEntries.AddRange(preFixedLogEntries);
                if (fixedLogEntries.Count > 1)
                {
                    for (int i = 0; i < fixedLogEntries.Count - 1; i++)
                    {
                        fixedLogEntries[i + 1].UpdateEntry(fixedLogEntries[i].BeforeQuantity, fixedLogEntries[i].ChangedQuantity);
                    }
                }
            }
            else
            {
                var preFixedLogEntries = await _inventoryLogEntryRepository.GetLatestLogEntries(removedLogEntry.ItemId, removedLogEntry.TrackingTime);
                preFixedLogEntries.Remove(removedLogEntry);

                fixedLogEntries.AddRange(preFixedLogEntries);
                fixedLogEntries[0].ResetQuantity();
                if (fixedLogEntries.Count > 1)
                {
                    for (int i = 0; i < fixedLogEntries.Count - 1; i++)
                    {
                        fixedLogEntries[i + 1].UpdateEntry(fixedLogEntries[i].BeforeQuantity, fixedLogEntries[i].ChangedQuantity);
                    }
                }
            }
            _service.AddEntries(fixedLogEntries);
        }

        else
        {
            removedLogEntry = _service.GetLogEntry(notification.ItemId, notification.ItemLotId, notification.Timestamp);
            if (removedLogEntry is null)
            {
                throw new EntityNotFoundException($"InventoryLogEntry of {notification} with {notification.Timestamp} not found.");
            }
            var previousLogEntry = await _service.GetPreviousLogEntry(removedLogEntry.ItemId, removedLogEntry.TrackingTime);

            if (previousLogEntry is not null)
            {
                var preFixedLogEntries = await _service.GetLatestLogEntries(previousLogEntry.ItemId, previousLogEntry.TrackingTime);
                preFixedLogEntries.Remove(removedLogEntry);

                fixedLogEntries.AddRange(preFixedLogEntries);
                if (fixedLogEntries.Count > 1)
                {
                    for (int i = 0; i < fixedLogEntries.Count - 1; i++)
                    {
                        fixedLogEntries[i + 1].UpdateEntry(fixedLogEntries[i].BeforeQuantity, fixedLogEntries[i].ChangedQuantity);
                    }
                }
            }
            else
            {
                var preFixedLogEntries = await _service.GetLatestLogEntries(removedLogEntry.ItemId, removedLogEntry.TrackingTime);
                preFixedLogEntries.Remove(removedLogEntry);

                fixedLogEntries = preFixedLogEntries;
                fixedLogEntries[0].ResetQuantity();
                if (fixedLogEntries.Count > 1)
                {
                    for (int i = 0; i < fixedLogEntries.Count - 1; i++)
                    {
                        fixedLogEntries[i + 1].UpdateEntry(fixedLogEntries[i].BeforeQuantity, fixedLogEntries[i].ChangedQuantity);
                    }
                }
            }
            _service.AddEntries(fixedLogEntries);
        }

        _inventoryLogEntryRepository.Delete(removedLogEntry);
        _inventoryLogEntryRepository.UpdateEntries(fixedLogEntries);
    }
}