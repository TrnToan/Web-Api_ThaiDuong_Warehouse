namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductIssues;

[DataContract]
public class AddFinishedProductIssueEntriesCommand : IRequest<bool>
{
    [DataMember]
    public string FinishedProductIssueId { get; private set; }
    [DataMember]
    public List<CreateFinishedProductIssueEntryViewModel> Entries { get; private set; }

    public AddFinishedProductIssueEntriesCommand(string finishedProductIssueId, List<CreateFinishedProductIssueEntryViewModel> entries)
    {
        FinishedProductIssueId = finishedProductIssueId;
        Entries = entries;
    }
}
