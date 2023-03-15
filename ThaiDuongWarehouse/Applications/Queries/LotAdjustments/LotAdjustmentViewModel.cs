namespace ThaiDuongWarehouse.Api.Applications.Queries.LotAdjustments;

public class LotAdjustmentViewModel
{
    public string LotId { get; private set; }
    public string NewPurchaseOrderNumber { get; private set; }
    public string OldPurchaseOrderNumber { get; private set; }
    public string? Note { get; private set; }
    public double BeforeQuantity { get; private set; }
    public double AfterQuantity { get; private set; }
    public bool IsConfirmed { get; private set; } = false;
    public DateTime Timestamp { get; private set; }
    public Employee Employee { get; private set; }
    public Item Item { get; private set; }
    public LotAdjustmentViewModel(string lotId, string newPurchaseOrderNumber, string oldPurchaseOrderNumber, string? note, 
        double beforeQuantity, double afterQuantity, bool isConfirmed, DateTime timestamp, Employee employee, Item item)
    {
        LotId = lotId;
        NewPurchaseOrderNumber = newPurchaseOrderNumber;
        OldPurchaseOrderNumber = oldPurchaseOrderNumber;
        Note = note;
        BeforeQuantity = beforeQuantity;
        AfterQuantity = afterQuantity;
        IsConfirmed = isConfirmed;
        Timestamp = timestamp;
        Employee = employee;
        Item = item;
    }
}
