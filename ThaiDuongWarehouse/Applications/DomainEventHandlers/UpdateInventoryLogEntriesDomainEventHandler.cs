using ThaiDuongWarehouse.Api.Applications.DomainEventHandlers.DataTransactionServices;
using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class UpdateInventoryLogEntriesDomainEventHandler : INotificationHandler<UpdateInventoryLogEntriesDomainEvent>
{
    private readonly IInventoryLogEntryRepository _inventoryLogEntryRepository;
    private readonly InventoryLogEntryService _service;

    public UpdateInventoryLogEntriesDomainEventHandler(IInventoryLogEntryRepository inventoryLogEntryRepository, 
        InventoryLogEntryService service)
    {
        _inventoryLogEntryRepository = inventoryLogEntryRepository;
        _service = service;
    }

    public async Task Handle(UpdateInventoryLogEntriesDomainEvent notification, CancellationToken cancellationToken)
    {
        var serviceLogEntry = _service.GetLogEntry(notification.ItemId, notification.ItemLotId, notification.Timestamp);        

        List<InventoryLogEntry> fixedLogEntries;
        if (serviceLogEntry is null)
        {
            var logEntry = await _inventoryLogEntryRepository.GetLogEntry(notification.ItemId, notification.ItemLotId, 
                notification.Timestamp);

            if (logEntry is null)
            {
                throw new EntityNotFoundException($"InventoryLogEntry with {notification.ItemLotId} at {notification.Timestamp} not found.");
            }
            logEntry.UpdateQuantity(notification.ChangedQuantity, notification.ReceivedQuantity);
            fixedLogEntries = await _inventoryLogEntryRepository.GetLatestLogEntries(notification.ItemId, logEntry.TrackingTime);
            
            for (int i = 0; i < fixedLogEntries.Count - 1; i++)
            {
                fixedLogEntries[i + 1].UpdateEntry(fixedLogEntries[i].BeforeQuantity, fixedLogEntries[i].ChangedQuantity);
            }
            _service.AddEntries(fixedLogEntries);
        }

        else
        {
            serviceLogEntry.UpdateQuantity(notification.ChangedQuantity, notification.ReceivedQuantity);
            var serviceLogEntries = _service.GetLogEntries(notification.ItemId, serviceLogEntry.TrackingTime);           
            if (serviceLogEntries.Count > 1)
            {
                fixedLogEntries = serviceLogEntries;
                for (int i = 0; i < fixedLogEntries.Count - 1; i++)
                {
                    fixedLogEntries[i + 1].UpdateEntry(fixedLogEntries[i].BeforeQuantity, fixedLogEntries[i].ChangedQuantity);
                }
            }
        }            

        _inventoryLogEntryRepository.UpdateEntries(_service.Entries);
    }
}