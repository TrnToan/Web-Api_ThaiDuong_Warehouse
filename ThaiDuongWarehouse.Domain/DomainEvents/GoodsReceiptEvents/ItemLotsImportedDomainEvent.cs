namespace ThaiDuongWarehouse.Domain.DomainEvents.GoodsReceiptEvents;
public class ItemLotsImportedDomainEvent : INotification
{
    public List<ItemLot> ItemLots { get; private set; }
    public ItemLotsImportedDomainEvent(List<ItemLot> itemLots)
    {
        ItemLots = itemLots;
    }
}
