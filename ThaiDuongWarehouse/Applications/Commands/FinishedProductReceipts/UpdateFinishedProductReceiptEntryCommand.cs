namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

[DataContract]
public class UpdateFinishedProductReceiptEntryCommand : IRequest<bool>
{
    [DataMember]
    public string FinishedProductReceiptId { get; private set; }
    [DataMember]
    public List<UpdateFinishedProductReceiptEntryViewModel> Entries { get; private set; }

    public UpdateFinishedProductReceiptEntryCommand(string finishedProductReceiptId, List<UpdateFinishedProductReceiptEntryViewModel> entries)
    {
        FinishedProductReceiptId = finishedProductReceiptId;
        Entries = entries;
    }
}
