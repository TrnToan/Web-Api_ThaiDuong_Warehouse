namespace ThaiDuongWarehouse.Domain.AggregateModels.AdjustmentAggregate;
public class SublotAdjustment
{
    public string LocationId { get; private set; }
    public double BeforeQuantityPerLocation { get; private set; }
    public double AfterQuantityPerLocation { get; private set; }

    public SublotAdjustment(string locationId, double beforeQuantityPerLocation, double afterQuantityPerLocation)
    {
        LocationId = locationId;
        BeforeQuantityPerLocation = beforeQuantityPerLocation;
        AfterQuantityPerLocation = afterQuantityPerLocation;
    }
}
