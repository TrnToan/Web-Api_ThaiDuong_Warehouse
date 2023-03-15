namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;
public class GoodsIssueEntry
{
    public int Id { get; set; }              // Unique value of GoodsIssueEntry Table
    public double? RequestedSublotSize { get; private set; }
    public double RequestedQuantity { get; private set; }
    public int GoodsIssueId { get; set; }    // Foreign Key
    public Item Item { get; private set; }
    public List<GoodsIssueLot> Lots { get; private set; }

    private GoodsIssueEntry() { }
    public GoodsIssueEntry(Item item, double? requestedSublotSize, double requestedQuantity) : this()
    {
        Item = item;
        RequestedSublotSize = requestedSublotSize;
        RequestedQuantity = requestedQuantity;
        Lots = new List<GoodsIssueLot>();
    }
    public void SetQuantity(double requestedQuantity)
    {
        RequestedQuantity = requestedQuantity;
    }
    public void AddLot(GoodsIssueLot goodsIssueLot)
    {
        if(goodsIssueLot.Quantity <= 0)
        {
            throw new WarehouseDomainException("GoodsIssue is not valid!");
        }
        Lots.Add(goodsIssueLot);
    }
}
