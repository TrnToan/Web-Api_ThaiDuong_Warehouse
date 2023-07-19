
namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsIssues;

public class GoodsIssueViewModel
{
    public string GoodsIssueId { get; private set; }
    public string Receiver { get; private set; }
    public DateTime Timestamp { get; private set; }
    public EmployeeViewModel Employee { get; private set; }
    public List<GoodsIssueEntryViewModel> Entries { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public GoodsIssueViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {    }
    public GoodsIssueViewModel(string goodsIssueId, string receiver, DateTime timestamp, EmployeeViewModel employee, 
        List<GoodsIssueEntryViewModel> entries)
    {
        GoodsIssueId = goodsIssueId;
        Receiver = receiver;
        Timestamp = timestamp;
        Employee = employee;
        Entries = entries;
    }
}
