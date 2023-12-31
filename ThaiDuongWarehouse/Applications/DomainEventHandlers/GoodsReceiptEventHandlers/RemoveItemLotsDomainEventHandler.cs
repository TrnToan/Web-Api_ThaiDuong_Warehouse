namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers.GoodsReceiptEventHandlers;

public class RemoveItemLotsDomainEventHandler : INotificationHandler<RemoveItemLotsDomainEvent>
{
    private readonly IItemLotRepository _itemLotRepository;

    public RemoveItemLotsDomainEventHandler(IItemLotRepository itemLotRepository)
    {
        _itemLotRepository = itemLotRepository;
    }

    public async Task Handle(RemoveItemLotsDomainEvent notification, CancellationToken cancellationToken)
    {
        List<ItemLot> lots = new();
        foreach (var lotId in notification.ItemLots.Select(l => l.GoodsReceiptLotId))
        {
            var itemLot = await _itemLotRepository.GetLotByLotId(lotId);
            if (itemLot is null)
            {
                throw new EntityNotFoundException($"Cannot found itemlot with Id {lotId}");
            }

            lots.Add(itemLot);
        }
        _itemLotRepository.RemoveLots(lots);
    }
}
