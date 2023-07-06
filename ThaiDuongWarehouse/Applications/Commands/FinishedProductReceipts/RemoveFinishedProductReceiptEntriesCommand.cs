namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

[DataContract]
public class RemoveFinishedProductReceiptEntriesCommand : IRequest<bool>
{
    [DataMember]
    public string FinishedProductReceiptId { get; set; }
    [DataMember]
    public List<RemoveFinishedProductEntryViewModel> Entries { get; set; }

    public RemoveFinishedProductReceiptEntriesCommand(string finishedProductReceiptId, List<RemoveFinishedProductEntryViewModel> entries)
    {
        FinishedProductReceiptId = finishedProductReceiptId;
        Entries = entries;
    }
}
