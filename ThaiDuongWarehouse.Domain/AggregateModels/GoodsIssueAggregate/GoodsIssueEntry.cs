namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;
public class GoodsIssueEntry
{
    public int Id { get; set; }              // Unique value of GoodsIssueEntry Table
    public double? RequestedSublotSize { get; private set; }
    public double RequestedQuantity { get; private set; }
    public string Unit { get; private set; }
    // Foreign Key
    public int GoodsIssueId { get; private set; }    
    public int ItemId { get; private set; }
    public Item Item { get; private set; }
    public List<GoodsIssueLot> Lots { get; private set; }

    private GoodsIssueEntry() { }
    public GoodsIssueEntry(Item item, string unit, double? requestedSublotSize, double requestedQuantity) : this()
    {
        Item = item;
        RequestedSublotSize = requestedSublotSize;
        RequestedQuantity = requestedQuantity;
        Unit = unit;
        Lots = new List<GoodsIssueLot>();     
    }
    public void UpdateEntry(double? requestedSublotSize, double requestedQuantity)
    {
        if (requestedSublotSize != null) 
        {
            RequestedSublotSize = requestedSublotSize;
        }

        RequestedQuantity = requestedQuantity;
    }
    public void AddLot(GoodsIssueLot lot)
    {
        if(lot.Quantity <= 0)
        {
            throw new WarehouseDomainException("Quantity is not valid.");
        }
        Lots.Add(lot);
    }
}
