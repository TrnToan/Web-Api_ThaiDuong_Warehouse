namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class LotAdjustedDomainEvent : INotification
{
    public string LotId { get; private set; }
    public double AfterQuantity { get; private set; }
    public string NewPurchaseOrderNumber { get; private set; }
    public LotAdjustedDomainEvent(string lotId, double afterQuantity, string newPurchaseOrderNumber)
    {
        LotId = lotId;
        AfterQuantity = afterQuantity;
        NewPurchaseOrderNumber = newPurchaseOrderNumber;
    }
}
