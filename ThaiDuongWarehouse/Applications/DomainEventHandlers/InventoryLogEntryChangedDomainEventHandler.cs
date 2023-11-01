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
        var modifiedLogEntry = await _inventoryLogEntryRepository.GetLogEntry(notification.ItemId, notification.OldItemLotId, 
            notification.Timestamp);
        
        if (modifiedLogEntry is null)
        {
            throw new EntityNotFoundException($"InventoryLogEntry of Itemlot {notification.OldItemLotId} cannot be found.");
        }

        modifiedLogEntry.ModifyLogEntry(notification.NewItemLotId);
        _inventoryLogEntryRepository.Update(modifiedLogEntry);
    }
}