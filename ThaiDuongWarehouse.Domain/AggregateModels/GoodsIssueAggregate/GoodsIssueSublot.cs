namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;
public class GoodsIssueSublot
{
    public string LocationId { get; set; }
    public double QuantityPerLocation { get; set; }

    public GoodsIssueSublot(string locationId, double quantityPerLocation)
    {
        LocationId = locationId;
        QuantityPerLocation = quantityPerLocation;
    }
}
