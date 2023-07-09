namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductReceipts;

public class FinishedProductReceiptViewModel
{
    public string FinishedProductReceiptId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public EmployeeViewModel Employee { get; private set; }
    public List<FinishedProductReceiptEntryViewModel> Entries { get; private set; }

    public FinishedProductReceiptViewModel(string finishedProductReceiptId, DateTime timestamp, EmployeeViewModel employee, 
        List<FinishedProductReceiptEntryViewModel> entries)
    {
        FinishedProductReceiptId = finishedProductReceiptId;
        Timestamp = timestamp;
        Employee = employee;
        Entries = entries;
    }
}
