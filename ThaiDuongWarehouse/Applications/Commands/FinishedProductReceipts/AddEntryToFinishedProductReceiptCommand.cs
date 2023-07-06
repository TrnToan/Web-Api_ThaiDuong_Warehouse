namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

[DataContract]
public class AddEntryToFinishedProductReceiptCommand : IRequest<bool>
{
    [DataMember]
    public string FinishedProductReceiptId { get; set; }
    [DataMember]
    public List<CreateFinishedProductReceiptEntryViewModel> Entries { get; set; }

    public AddEntryToFinishedProductReceiptCommand(string finishedProductReceiptId, List<CreateFinishedProductReceiptEntryViewModel> entries)
    {
        FinishedProductReceiptId = finishedProductReceiptId;
        Entries = entries;
    }
}
