namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsIssues;

public class GoodsIssueLotViewModel
{
    public string GoodsIssueLotId { get; private set; }
    public double Quantity { get; private set; }
    public EmployeeViewModel Employee { get; private set; }
    public List<GoodsIssueSublotViewModel> Sublots { get; private set; }
    public string? Note { get; private set; }  
    public GoodsIssueLotViewModel(string goodsIssueLotId, double quantity, EmployeeViewModel employee, string? note, 
        List<GoodsIssueSublotViewModel> sublots)
    {
        GoodsIssueLotId = goodsIssueLotId;
        Quantity = quantity;
        Employee = employee;
        Note = note;
        Sublots = sublots;
    }
}
