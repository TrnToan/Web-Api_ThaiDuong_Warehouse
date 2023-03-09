namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;
public class GoodsIssueLot
{
    public string GoodsIssueLotId { get; private set; }
    public int EmployeeId { get; private set; }              //ForeignKey
    public double Quantity { get; private set; }
    public double? SublotSize { get; private set; }
    public string? Note { get; private set; }
    public Employee Employee { get; private set; }

    public GoodsIssueLot(string goodsIssueLotId, double quantity, double? sublotSize, string? note, 
        int employeeId)
    {
        GoodsIssueLotId = goodsIssueLotId;
        Quantity = quantity;
        SublotSize = sublotSize;
        Note = note;
        EmployeeId = employeeId;
    }
}
