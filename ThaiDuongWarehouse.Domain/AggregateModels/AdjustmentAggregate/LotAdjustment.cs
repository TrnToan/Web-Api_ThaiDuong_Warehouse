namespace ThaiDuongWarehouse.Domain.AggregateModels.AdjustmentAggregate;

public class LotAdjustment : Entity, IAggregateRoot
{
    public string LotId { get; private set; }
    public string OldPurchaseOrderNumber { get; private set; }
    public string NewPurchaseOrderNumber { get; private set; }
    public double BeforeQuantity {  get; private set; }
    public double AfterQuantity { get; private set; }
    public string Unit { get; private set; }
    public bool IsConfirmed { get; private set; } = false;
    public DateTime Timestamp { get; private set; } 
    public string? Note { get; private set; }
    public int ItemId { get; private set; }                 
    public int EmployeeId { get; private set; }              

    public Employee Employee { get; private set; }
    public Item Item { get; private set; }

    public LotAdjustment(string lotId, string oldPurchaseOrderNumber, string newPurchaseOrderNumber, double beforeQuantity, 
        double afterQuantity, string unit, bool isConfirmed, DateTime timestamp, string? note, 
        int itemId, int employeeId)
    {
        LotId = lotId;
        OldPurchaseOrderNumber = oldPurchaseOrderNumber;
        NewPurchaseOrderNumber = newPurchaseOrderNumber;
        BeforeQuantity = beforeQuantity;
        AfterQuantity = afterQuantity;
        Unit = unit;
        IsConfirmed = isConfirmed;
        Timestamp = timestamp;
        Note = note;
        ItemId = itemId;
        EmployeeId = employeeId;
    }

    public LotAdjustment(string lotId, string oldPurchaseOrderNumber, double beforeQuantity, string unit, string? note,
        DateTime timestamp, int itemId, int employeeId)
    {
        LotId = lotId;
        OldPurchaseOrderNumber = oldPurchaseOrderNumber;
        Note = note;
        BeforeQuantity = beforeQuantity;
        Unit = unit;
        Timestamp = timestamp;  
        ItemId = itemId;
        EmployeeId = employeeId;
    }

    public void Update(double quantity, string purchaseOrderNumber)
    {
        AfterQuantity = quantity;
        NewPurchaseOrderNumber = purchaseOrderNumber;
    }
    public void Confirm(string lotId, string itemId, string unit, double beforeQuantity , double afterQuanity, 
        string newPurchaseOrderNumber)
    {
        IsConfirmed = true;
        Timestamp = DateTime.Now;
        this.AddDomainEvent(new LotAdjustedDomainEvent(lotId, itemId, beforeQuantity, afterQuanity, unit, 
            newPurchaseOrderNumber, Timestamp));
    }
}
