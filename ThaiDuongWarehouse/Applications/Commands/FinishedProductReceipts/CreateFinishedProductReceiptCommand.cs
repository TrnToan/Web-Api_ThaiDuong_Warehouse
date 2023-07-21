namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

[DataContract]
public class CreateFinishedProductReceiptCommand : IRequest<bool>
{
    [DataMember]
    public string FinishedProductReceiptId { get; private set; }
    [DataMember]
    public string EmployeeId { get; private set; }
    [DataMember]
    public List<CreateFinishedProductReceiptEntryViewModel> Entries { get; private set; }

    public CreateFinishedProductReceiptCommand(string finishedProductReceiptId, string employeeId, 
        List<CreateFinishedProductReceiptEntryViewModel> entries)
    {
        FinishedProductReceiptId = finishedProductReceiptId;
        EmployeeId = employeeId;
        Entries = entries;
    }
}
