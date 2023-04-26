using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;
using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class InventoryLogEntryChangedDomainEventHandler : INotificationHandler<InventoryLogEntryChangedDomainEvent>
{
    private readonly IInventoryLogEntryRepository _inventoryLogEntryRepository;
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IItemRepository _itemRepository;
    private readonly InventoryLogEntryService _service;
    public InventoryLogEntryChangedDomainEventHandler(IInventoryLogEntryRepository inventoryLogEntryRepository, 
        IItemLotRepository itemLotRepository, IItemRepository itemRepository, InventoryLogEntryService service)
    {
        _inventoryLogEntryRepository = inventoryLogEntryRepository;
        _itemLotRepository = itemLotRepository;
        _itemRepository = itemRepository;
        _service = service;
    }

    public async Task Handle(InventoryLogEntryChangedDomainEvent notification, CancellationToken cancellationToken)
    {        
        Item? item = await _itemRepository.GetItemByEntityId(notification.ItemId);
        if (item is null)
        {
            throw new EntityNotFoundException($"Item doesn't exist");
        }

        InventoryLogEntry? latestEntry1 = await _inventoryLogEntryRepository.GetLatestLogEntry(notification.ItemId);
        InventoryLogEntry? latestEntry2 = _service.FindEntry(notification.ItemId);

        double beforeQuantity = 0;
        if (latestEntry1 is null && latestEntry2 is null)
        {
            IEnumerable<ItemLot> itemLots = await _itemLotRepository.GetLotsByItemId(item.ItemId, item.Unit);
            List<ItemLot> unIsolatedItemLots = itemLots.Where(lot => lot.IsIsolated == false).ToList();
            beforeQuantity = unIsolatedItemLots.Sum(x => x.Quantity);     
        }
        else if (latestEntry1 is not null && latestEntry2 is null)
        {
            beforeQuantity = latestEntry1.BeforeQuantity + latestEntry1.ChangedQuantity;
        }
        else 
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            beforeQuantity = latestEntry2.BeforeQuantity + latestEntry2.ChangedQuantity;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }                
        InventoryLogEntry newEntry = new (notification.ItemId, notification.ItemLotId, notification.Timestamp,
            beforeQuantity, notification.Quantity, item.Unit);

        _service.AddEntry(newEntry);
        _inventoryLogEntryRepository.Add(newEntry);
    }
}
