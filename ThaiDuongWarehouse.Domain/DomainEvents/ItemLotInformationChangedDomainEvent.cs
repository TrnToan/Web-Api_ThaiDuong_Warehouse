namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class ItemLotInformationChangedDomainEvent : INotification
{
    public string ItemLotId { get; private set; }
    public double Quantity { get; private set; }
    public ItemLotInformationChangedDomainEvent(string itemLotId, double quantity)
    {
        ItemLotId = itemLotId;
        Quantity = quantity;
    }
}
