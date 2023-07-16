namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;
public class GoodsReceiptSublot
{
    public string LocationId { get; set; }
    public double QuantityPerLocation { get; set; }

    public GoodsReceiptSublot(string locationId, double quantityPerLocation)
    {
        LocationId = locationId;
        QuantityPerLocation = quantityPerLocation;
    }
}
