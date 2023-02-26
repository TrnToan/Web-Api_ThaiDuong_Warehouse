using ThaiDuongWarehouse.Domain.DomainEvents;

namespace ThaiDuongWarehouse.Domain.AggregateModels.AdjustmentAggregate
{
    public class LotAdjustment : Entity, IAggregateRoot
    {
        public string LotId { get; private set; }
        public string NewPurchaseOrderNumber { get; private set; }
        public string OldPurchaseOrderNumber { get; private set; }
        public string? Note { get; private set; }
        public double BeforeQuantity {  get; private set; }
        public double AfterQuantity { get; private set; }
        public bool IsConfirmed { get; private set; }
        public Employee Employee { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Item Item { get; private set; }

        public LotAdjustment(string lotId, Employee employee, DateTime timestamp, 
            double beforeQuantity, double afterQuantity, string newPurchaseOrderNumber, string oldPurchaseOrderNumber,
            Item item)
        {
            LotId = lotId;
            Employee = employee;
            Timestamp = timestamp;
            BeforeQuantity = beforeQuantity;
            AfterQuantity = afterQuantity;
            NewPurchaseOrderNumber = newPurchaseOrderNumber;
            OldPurchaseOrderNumber = oldPurchaseOrderNumber;
            Item = item;
        }
        public void Update(double quantity, string purchaseOrderNumber)
        {
            AfterQuantity = quantity;
            NewPurchaseOrderNumber = purchaseOrderNumber;

            this.AddDomainEvent(new LotChangedDomainEvent(LotId, AfterQuantity, NewPurchaseOrderNumber));
        }
        public void Confirm(Employee employee)
        {
            Employee = employee;
            this.AddDomainEvent(new LotAdjustedDomainEvent(IsConfirmed));
        }
    }
}
