namespace ThaiDuongWarehouse.Domain.DomainEvents
{
    public class LotChangedDomainEvent : INotification
    {
        public string LotId { get; private set; }
        public double AfterQuantity { get; private set; }
        public string NewPurchaseOrderNumber { get; private set; }

        public LotChangedDomainEvent(string lotId, double afterQuantity, string newPurchaseOrderNumber)
        {
            LotId = lotId;
            AfterQuantity = afterQuantity;
            NewPurchaseOrderNumber = newPurchaseOrderNumber;
        }
    }
}
