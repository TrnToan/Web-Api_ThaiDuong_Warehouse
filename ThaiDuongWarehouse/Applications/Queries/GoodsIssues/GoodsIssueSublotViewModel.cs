namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsIssues;

public class GoodsIssueSublotViewModel
{
    public string LocationId { get; set; }
    public double QuantityPerLocation { get; set; }

    public GoodsIssueSublotViewModel(string locationId, double quantityPerLocation)
    {
        LocationId = locationId;
        QuantityPerLocation = quantityPerLocation;
    }
}
