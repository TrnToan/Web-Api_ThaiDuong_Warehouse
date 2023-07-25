namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

public class CreateSublotAdjustmentViewModel
{
    public string LocationId { get; private set; }
    public double NewQuantityPerLocation { get; private set; }

    public CreateSublotAdjustmentViewModel(string locationId, double newQuantityPerLocation)
    {
        LocationId = locationId;
        NewQuantityPerLocation = newQuantityPerLocation;
    }
}
