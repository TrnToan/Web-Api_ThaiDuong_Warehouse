namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class ItemLotsImportedDomainEvent : INotification
{
	public IEnumerable<ItemLot> ItemLots { get; private set; }
	public ItemLotsImportedDomainEvent(IEnumerable<ItemLot> itemLots)
    {
        ItemLots = itemLots;
    }
}
