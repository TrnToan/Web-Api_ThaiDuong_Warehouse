namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class LotAdjustedDomainEvent : INotification
{
    public string LotId { get; private set; }
    public string ItemId { get; private set; }
    public double BeforeQuantity { get; private set; }
    public double AfterQuantity { get; private set; }
    public string Unit { get; private set; }
    public string NewPurchaseOrderNumber { get; private set; }
    public DateTime Timestamp { get; private set; }
    public LotAdjustedDomainEvent(string lotId, string itemId, double beforeQuantity , double afterQuantity, string unit,
        string newPurchaseOrderNumber, DateTime timestamp)
    {
        LotId = lotId;
        ItemId = itemId;
        BeforeQuantity = beforeQuantity;
        AfterQuantity = afterQuantity;
        Unit = unit;
        NewPurchaseOrderNumber = newPurchaseOrderNumber;
        Timestamp = timestamp;
    }
}