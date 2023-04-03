namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsIssues;

public class GoodsIssueLotViewModel
{
    public string GoodsIssueLotId { get; private set; }
    public double Quantity { get; private set; }
    public double? SublotSize { get; private set; }
    public EmployeeViewModel Employee { get; private set; }
    public string? Note { get; private set; }  
    public GoodsIssueLotViewModel(string goodsIssueLotId, double quantity, double? sublotSize, 
        EmployeeViewModel employee, string? note)
    {
        GoodsIssueLotId = goodsIssueLotId;
        Quantity = quantity;
        SublotSize = sublotSize;     
        Employee = employee;
        Note = note;
    }
}
