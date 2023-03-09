namespace ThaiDuongWarehouse.Domain.AggregateModels.AdjustmentAggregate;

public class LotAdjustment : Entity, IAggregateRoot
{
    public string LotId { get; private set; }
    public int ItemId { get; private set; }                  // ForeignKey
    public int EmployeeId { get; private set; }              // ForeignKey
    public string NewPurchaseOrderNumber { get; private set; }
    public string OldPurchaseOrderNumber { get; private set; }
    public string? Note { get; private set; }
    public double BeforeQuantity {  get; private set; }
    public double AfterQuantity { get; private set; }
    public bool IsConfirmed { get; private set; } = false;
    public DateTime Timestamp { get; private set; }
    public Employee Employee { get; private set; }
    public Item Item { get; private set; }

    public LotAdjustment(string lotId, string newPurchaseOrderNumber, string oldPurchaseOrderNumber, 
        string? note, double beforeQuantity, double afterQuantity, bool isConfirmed, DateTime timestamp, 
        int employeeId, int itemId)
    {
        LotId = lotId;
        NewPurchaseOrderNumber = newPurchaseOrderNumber;
        OldPurchaseOrderNumber = oldPurchaseOrderNumber;
        Note = note;
        BeforeQuantity = beforeQuantity;
        AfterQuantity = afterQuantity;
        IsConfirmed = isConfirmed;
        Timestamp = timestamp;
        EmployeeId = employeeId;
        ItemId = itemId;
    }

    public void Update(double quantity, string purchaseOrderNumber)
    {
        AfterQuantity = quantity;
        NewPurchaseOrderNumber = purchaseOrderNumber;

        this.AddDomainEvent(new LotChangedDomainEvent(LotId, AfterQuantity, NewPurchaseOrderNumber));
    }
    public void Confirm(Employee employee, DateTime timestamp)
    {
        IsConfirmed = true;
        Employee = employee;
        Timestamp = timestamp;
        this.AddDomainEvent(new LotAdjustedDomainEvent(IsConfirmed));
    }
}
