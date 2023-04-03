namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;
public class GoodsIssueLot
{
    public string GoodsIssueLotId { get; private set; }
    public double Quantity { get; private set; }
    public double? SublotSize { get; private set; }
    public string? Note { get; private set; }
    // ForeignKey
    public int EmployeeId { get; private set; }
    public Employee Employee { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private GoodsIssueLot() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public GoodsIssueLot(string goodsIssueLotId, double quantity, double? sublotSize, string? note,
        int employeeId) : this()
    {
        GoodsIssueLotId = goodsIssueLotId;
        Quantity = quantity;
        SublotSize = sublotSize;
        Note = note;
        EmployeeId = employeeId;
    }
}
