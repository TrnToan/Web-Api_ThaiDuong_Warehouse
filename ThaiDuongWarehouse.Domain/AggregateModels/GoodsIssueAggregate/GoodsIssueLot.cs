namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;
public class GoodsIssueLot
{
    public string GoodsIssueLotId { get; private set; }
    public double Quantity { get; private set; }
    public double? SublotSize { get; private set; }
    public string? Note { get; private set; }
    // ForeignKey
    public int GoodsIssueEntryId { get; private set; }
    public int EmployeeId { get; private set; }
    public Employee Employee { get; private set; }

    private GoodsIssueLot() { }

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
