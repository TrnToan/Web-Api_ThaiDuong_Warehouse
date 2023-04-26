namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class CreateItemWithNewUnitDomainEvent : INotification
{
    public string ItemId { get; private set; }
    public string ItemClassId { get; private set; }
    public string Unit { get; private set; }
    public CreateItemWithNewUnitDomainEvent(string itemId, string itemClassId, string unit)
    {
        ItemId = itemId;
        ItemClassId = itemClassId;
        Unit = unit;
    }
}
