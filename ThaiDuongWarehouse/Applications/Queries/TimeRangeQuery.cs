namespace ThaiDuongWarehouse.Api.Applications.Queries;

public class TimeRangeQuery : Query
{
    public DateTime StartTime { get; set; } = DateTime.MinValue;
    public DateTime EndTime { get; set; } = DateTime.Now;
}
