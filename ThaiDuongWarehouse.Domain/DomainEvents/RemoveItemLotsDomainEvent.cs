namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class RemoveItemLotsDomainEvent : INotification
{
    public List<GoodsReceiptLot> ItemLots { get; private set; }

    public RemoveItemLotsDomainEvent(List<GoodsReceiptLot> itemLots)
    {
        ItemLots = itemLots;
    }
}
