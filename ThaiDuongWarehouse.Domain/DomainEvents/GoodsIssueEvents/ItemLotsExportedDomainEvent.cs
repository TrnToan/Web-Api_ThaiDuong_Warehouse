namespace ThaiDuongWarehouse.Domain.DomainEvents.GoodsIssueEvents;
public class ItemLotsExportedDomainEvent : INotification
{
    public List<ItemLot> ItemLots { get; private set; }

    public ItemLotsExportedDomainEvent(List<ItemLot> itemLots)
    {
        ItemLots = itemLots;
    }
}
