namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductIssues;

[DataContract]
public class CreateFinishedProductIssueCommand : IRequest<bool>
{
    [DataMember]
    public string FinishedProductIssueId { get; private set; }
    [DataMember]
    public string? Receiver { get; private set; }
    [DataMember]
    public string EmployeeId { get; private set; }
    [DataMember]
    public DateTime Timestamp { get; private set; } = DateTime.Now;
    [DataMember]
    public List<CreateFinishedProductIssueEntryViewModel> Entries { get; private set; }

    public CreateFinishedProductIssueCommand(string finishedProductIssueId, string employeeId, string? receiver,
        List<CreateFinishedProductIssueEntryViewModel> entries)
    {
        FinishedProductIssueId = finishedProductIssueId;
        Receiver = receiver;
        EmployeeId = employeeId;
        Entries = entries;
    }
}
