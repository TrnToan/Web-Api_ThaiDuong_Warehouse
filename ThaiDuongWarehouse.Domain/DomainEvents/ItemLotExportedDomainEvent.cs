namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class ItemLotExportedDomainEvent : INotification
{
    public List<ItemLot> ItemLots { get; private set; }

    public ItemLotExportedDomainEvent(List<ItemLot> itemLots)
    {
        ItemLots = itemLots;
    }
}
