namespace ThaiDuongWarehouse.Domain.AggregateModels.AdjustmentAggregate;

public class LotAdjustment : Entity, IAggregateRoot
{
    public string LotId { get; private set; }
    public double BeforeQuantity {  get; private set; }
    public double AfterQuantity { get; private set; }
    public bool IsConfirmed { get; private set; } = false;
    public DateTime Timestamp { get; private set; } 
    public string? Note { get; private set; }
    public int ItemId { get; private set; }                 
    public int EmployeeId { get; private set; }              

    public Employee Employee { get; private set; }
    public Item Item { get; private set; }

    public LotAdjustment(string lotId, double beforeQuantity, double afterQuantity, bool isConfirmed, DateTime timestamp, 
        string? note, int itemId, int employeeId)
    {
        LotId = lotId;
        BeforeQuantity = beforeQuantity;
        AfterQuantity = afterQuantity;
        IsConfirmed = isConfirmed;
        Timestamp = timestamp;
        Note = note;
        ItemId = itemId;
        EmployeeId = employeeId;
    }

    public LotAdjustment(string lotId, double beforeQuantity, string? note, DateTime timestamp, int itemId, int employeeId)
    {
        LotId = lotId;
        Note = note;
        BeforeQuantity = beforeQuantity;
        Timestamp = timestamp;  
        ItemId = itemId;
        EmployeeId = employeeId;
    }

    public void Update(double quantity)
    {
        AfterQuantity = quantity;
    }
    public void Confirm(string lotId, string itemId, double beforeQuantity , double afterQuanity, string unit)
    {
        IsConfirmed = true;
        Timestamp = DateTime.Now;
        AddDomainEvent(new LotAdjustedDomainEvent(lotId, itemId, beforeQuantity, afterQuanity, Timestamp, unit));
    }
}
