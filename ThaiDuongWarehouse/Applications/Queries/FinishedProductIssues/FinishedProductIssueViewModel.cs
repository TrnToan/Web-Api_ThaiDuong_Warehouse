namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductIssues;

public class FinishedProductIssueViewModel
{
    public string FinishedProductIssueId { get; private set; }
    public string Receiver { get; private set; }
    public DateTime Timestamp { get; private set; }
    public EmployeeViewModel Employee { get; private set; }
    public List<FinishedProductIssueEntryViewModel> Entries { get; private set; }

    public FinishedProductIssueViewModel(string finishedProductIssueId, string receiver, DateTime timestamp, EmployeeViewModel employee, 
        List<FinishedProductIssueEntryViewModel> entries)
    {
        FinishedProductIssueId = finishedProductIssueId;
        Receiver = receiver;
        Timestamp = timestamp;
        Employee = employee;
        Entries = entries;
    }
}
