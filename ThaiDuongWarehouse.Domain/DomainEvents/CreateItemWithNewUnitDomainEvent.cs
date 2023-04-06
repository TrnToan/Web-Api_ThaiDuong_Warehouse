namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class CreateItemWithNewUnitDomainEvent : INotification
{
    public string ItemId { get; private set; }
    public string ItemClassId { get; private set; }
    public string ItemName { get; private set; }
    public string Unit { get; private set; }
    public CreateItemWithNewUnitDomainEvent(string itemId, string itemClassId, string itemName, string unit)
    {
        ItemId = itemId;
        ItemClassId = itemClassId;
        ItemName = itemName;
        Unit = unit;
    }
}
