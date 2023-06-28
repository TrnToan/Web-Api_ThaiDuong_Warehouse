namespace ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductIssueAggregate;
public class FinisedProductIssue : Entity, IAggregateRoot
{
    public string FinishedProductIssueId { get; private set; }
    public string? Receiver { get; private set; }
    public DateTime Timestamp { get; private set; }

    public int EmployeeId { get; private set; }
    public Employee Employee { get; private set; }
    public List<FinisedProductIssueEntry> Entries { get; private set; }

    public FinisedProductIssue(string finishedProductIssueId, string? receiver, DateTime timestamp, int employeeId)
    {
        FinishedProductIssueId = finishedProductIssueId;
        Receiver = receiver;
        Timestamp = timestamp;
        EmployeeId = employeeId;
        Entries = new List<FinisedProductIssueEntry>();
    }
}
