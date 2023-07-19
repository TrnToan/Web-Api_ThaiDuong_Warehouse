using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

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
        InventoryLogEntry removedLogEntry;
        List<InventoryLogEntry> fixedLogEntries = new ();

        var serviceLogEntry = _service.GetLogEntry(notification.ItemLotId, notification.Timestamp);
        if (serviceLogEntry is null)
        {
            var logEntry = await _inventoryLogEntryRepository.GetLogEntry(notification.ItemLotId, notification.Timestamp);
            if (logEntry is null)
            {
                throw new EntityNotFoundException($"InventoryLogEntry of {notification} with {notification.Timestamp} not found.");
            }
            removedLogEntry = logEntry;
            var previousLogEntry = await _inventoryLogEntryRepository.GetPreviousLogEntry(logEntry.ItemId, logEntry.TrackingTime);
            
            if (previousLogEntry is not null)
            {
                var preFixedLogEntries = await _inventoryLogEntryRepository.GetLatestLogEntries(previousLogEntry.ItemId, previousLogEntry.TrackingTime);
                preFixedLogEntries.Remove(logEntry);

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
                var preFixedLogEntries = await _inventoryLogEntryRepository.GetLatestLogEntries(logEntry.ItemId, logEntry.TrackingTime);
                preFixedLogEntries.Remove(logEntry);

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
            var logEntry = _service.GetLogEntry(notification.ItemLotId, notification.Timestamp);
            if (logEntry is null)
            {
                throw new EntityNotFoundException($"InventoryLogEntry of {notification} with {notification.Timestamp} not found.");
            }
            removedLogEntry = logEntry;
            var previousLogEntry = await _service.GetPreviousLogEntry(logEntry.ItemId, logEntry.TrackingTime);

            if (previousLogEntry is not null)
            {
                var preFixedLogEntries = await _service.GetLatestLogEntries(previousLogEntry.ItemId, previousLogEntry.TrackingTime);
                preFixedLogEntries.Remove(logEntry);

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
                var preFixedLogEntries = await _service.GetLatestLogEntries(logEntry.ItemId, logEntry.TrackingTime);
                preFixedLogEntries.Remove(logEntry);

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