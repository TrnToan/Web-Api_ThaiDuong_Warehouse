namespace ThaiDuongWarehouse.Domain.DomainEvents
{
    public class ItemCreatedDomainEvent : INotification
    {
        public Item Item { get; private set; }
        public ItemCreatedDomainEvent(Item item)
        {
            Item = item;
        }
    }
}
