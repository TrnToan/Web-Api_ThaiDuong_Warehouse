namespace ThaiDuongWarehouse.Api.Applications.Queries.LotAdjustments;

public class LotAdjustmentViewModel
{
    public string LotId { get; private set; }
    public double BeforeQuantity { get; private set; }
    public double AfterQuantity { get; private set; }
    public ItemViewModel Item { get; private set; }
    public EmployeeViewModel Employee { get; private set; }
    public string? Note { get; private set; }
    public List<SublotAdjustmentViewModel> SublotAdjustments { get; private set; }

    public LotAdjustmentViewModel(string lotId, double beforeQuantity, double afterQuantity, ItemViewModel item, 
        EmployeeViewModel employee, string? note, List<SublotAdjustmentViewModel> sublotAdjustments)
    {
        LotId = lotId;
        BeforeQuantity = beforeQuantity;
        AfterQuantity = afterQuantity;
        Item = item;
        Employee = employee;
        Note = note;
        SublotAdjustments = sublotAdjustments;
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public LotAdjustmentViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {   }
}
