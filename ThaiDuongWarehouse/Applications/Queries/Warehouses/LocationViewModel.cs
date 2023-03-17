namespace ThaiDuongWarehouse.Api.Applications.Queries.Warehouses;

public class LocationViewModel
{
    public string LocationId { get; private set; }

    public LocationViewModel(string locationId)
    {
        LocationId = locationId;
    }
}
