namespace ThaiDuongWarehouse.Api.Applications.Queries.LotAdjustments;

public class LotAdjustmentViewModel
{
    public string LotId { get; private set; }
    public double BeforeQuantity { get; private set; }
    public double AfterQuantity { get; private set; }
    public string OldPurchaseOrderNumber { get; private set; }
    public string NewPurchaseOrderNumber { get; private set; }
    public ItemViewModel Item { get; private set; }
    public EmployeeViewModel Employee { get; private set; }
    public string? Note { get; private set; }
    public LotAdjustmentViewModel(string lotId, double beforeQuantity, double afterQuantity, string oldPurchaseOrderNumber, 
        string newPurchaseOrderNumber, ItemViewModel item, EmployeeViewModel employee, string? note)
    {
        LotId = lotId;
        BeforeQuantity = beforeQuantity;
        AfterQuantity = afterQuantity;
        OldPurchaseOrderNumber = oldPurchaseOrderNumber;
        NewPurchaseOrderNumber = newPurchaseOrderNumber;
        Item = item;
        Employee = employee;
        Note = note;
    }
}
