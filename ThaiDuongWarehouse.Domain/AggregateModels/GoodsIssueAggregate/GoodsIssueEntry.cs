namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;
public class GoodsIssueEntry
{
    public int Id { get; set; }              // Unique value of GoodsIssueEntry Table
    public double RequestedQuantity { get; private set; }
    // Foreign Key
    public int GoodsIssueId { get; private set; }    
    public int ItemId { get; private set; }
    public Item Item { get; private set; }
    public List<GoodsIssueLot> Lots { get; private set; }

    private GoodsIssueEntry() { }
    public GoodsIssueEntry(Item item, double requestedQuantity) : this()
    {
        Item = item;
        RequestedQuantity = requestedQuantity;
        Lots = new List<GoodsIssueLot>();     
    }
    public void UpdateEntry(double requestedQuantity)
    {
        RequestedQuantity = requestedQuantity;
    }
    public void AddLot(GoodsIssueLot lot)
    {
        var existedLot = Lots.Find(l => l.GoodsIssueLotId == lot.GoodsIssueLotId);
        if (existedLot is not null)
        {
            throw new WarehouseDomainException($"GoodsIssueLot with Id {lot.GoodsIssueLotId} already existed in this goodsIssue.");
        }
        Lots.Add(lot);
    }
}
