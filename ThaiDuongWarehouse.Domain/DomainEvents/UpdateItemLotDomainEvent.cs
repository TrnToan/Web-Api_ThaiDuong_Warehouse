namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class UpdateItemLotDomainEvent : INotification
{
    public string ItemLotId { get; private set; }
    public double Quantity { get; private set; }

    public UpdateItemLotDomainEvent(string itemLotId, double quantity)
    {
        ItemLotId = itemLotId;
        Quantity = quantity;
    }
}
