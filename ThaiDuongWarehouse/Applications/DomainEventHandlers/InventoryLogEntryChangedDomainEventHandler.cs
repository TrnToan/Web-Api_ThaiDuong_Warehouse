using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;
using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class InventoryLogEntryChangedDomainEventHandler : INotificationHandler<InventoryLogEntryChangedDomainEvent>
{
    private readonly IInventoryLogEntryRepository _inventoryLogEntryRepository;
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IItemRepository _itemRepository;
    private InventoryLogEntryService _service;
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
        InventoryLogEntry? latestEntry1 = await _inventoryLogEntryRepository.GetLatestLogEntry(notification.ItemId);
        InventoryLogEntry? latestEntry2 = _service.FindEntry(notification.ItemId);

        double tempQuantity = 0;
        if (latestEntry1 is null && latestEntry2 is null)
        {
            IEnumerable<ItemLot> itemLots = await _itemLotRepository.GetLotsByItemId(item.ItemId, item.Unit);
            List<ItemLot> unIsolatedItemLots = itemLots.Where(lot => lot.IsIsolated == false).ToList();
            tempQuantity = unIsolatedItemLots.Sum(x => x.Quantity);     
        }
        else if (latestEntry1 is not null && latestEntry2 is null)
        {
            tempQuantity = latestEntry1.BeforeQuantity + latestEntry1.ChangedQuantity;
        }
        else 
        {
            tempQuantity = latestEntry2.BeforeQuantity + latestEntry2.ChangedQuantity;
        }                
        InventoryLogEntry newEntry = new (notification.ItemId, notification.ItemLotId, notification.Timestamp,
            tempQuantity, notification.Quantity, item?.Unit);

        _service.AddEntry(newEntry);
        _inventoryLogEntryRepository.Add(newEntry);
    }
}
