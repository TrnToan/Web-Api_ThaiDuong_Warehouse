namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class ItemLotsExportedDomainEvent : INotification
{
    public List<ItemLot> ItemLots { get; private set; }
	public ItemLotsExportedDomainEvent(List<ItemLot> itemLots)
	{
		ItemLots = itemLots;
	}
}
