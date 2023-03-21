using Unit = ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Unit;

namespace ThaiDuongWarehouse.Api.Applications.Queries.LotAdjustments;

public class LotAdjustmentViewModel
{
    public string LotId { get; private set; }
    public string ItemId { get; private set; }
    public double BeforeQuantity { get; private set; }
    public double AfterQuantity { get; private set; }
    public string OldPurchaseOrderNumber { get; private set; }
    public string NewPurchaseOrderNumber { get; private set; }
    public Unit Unit { get; private set; }
    public string EmployeeName { get; private set; }
    public string? Note { get; private set; }
    public LotAdjustmentViewModel(string lotId, string itemId, double beforeQuantity, double afterQuantity, string oldPurchaseOrderNumber, string newPurchaseOrderNumber, Unit unit, string employeeName, string? note)
    {
        LotId = lotId;
        ItemId = itemId;
        BeforeQuantity = beforeQuantity;
        AfterQuantity = afterQuantity;
        OldPurchaseOrderNumber = oldPurchaseOrderNumber;
        NewPurchaseOrderNumber = newPurchaseOrderNumber;
        Unit = unit;
        EmployeeName = employeeName;
        Note = note;
    }
}
