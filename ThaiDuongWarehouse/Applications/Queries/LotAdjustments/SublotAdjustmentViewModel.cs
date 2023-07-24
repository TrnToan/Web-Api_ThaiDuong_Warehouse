namespace ThaiDuongWarehouse.Api.Applications.Queries.LotAdjustments;

public class SublotAdjustmentViewModel
{
    public string LocationId { get; private set; }
    public double BeforeQuantityPerLocation { get; private set; }
    public double AfterQuantityPerLocation { get; private set; }

    public SublotAdjustmentViewModel(string locationId, double beforeQuantityPerLocation, double afterQuantityPerLocation)
    {
        LocationId = locationId;
        BeforeQuantityPerLocation = beforeQuantityPerLocation;
        AfterQuantityPerLocation = afterQuantityPerLocation;
    }
}
