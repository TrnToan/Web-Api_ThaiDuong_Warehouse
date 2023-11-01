namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class ItemLotsImportedDomainEventHandler : INotificationHandler<ItemLotsImportedDomainEvent>
{
    private readonly IItemLotRepository _itemLotRepository;
    public ItemLotsImportedDomainEventHandler(IItemLotRepository itemLotRepository)
    {
        _itemLotRepository = itemLotRepository;
    }

    public async Task Handle(ItemLotsImportedDomainEvent notification, CancellationToken cancellationToken)
    {
        foreach (var lotId in notification.ItemLots.Select(lot => lot.LotId))
        {
            var isExistedItemLot = await _itemLotRepository.GetLotByLotId(lotId);
            if (isExistedItemLot is not null)
            {
                throw new DuplicateRecordException(nameof(ItemLot), isExistedItemLot.LotId);
            }
        }
        _itemLotRepository.Addlots(notification.ItemLots);
        await Task.CompletedTask;
    }
}