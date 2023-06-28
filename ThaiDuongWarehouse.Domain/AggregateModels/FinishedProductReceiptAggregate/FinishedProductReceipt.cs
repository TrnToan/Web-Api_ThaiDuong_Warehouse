namespace ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductReceiptAggregate;
public class FinishedProductReceipt : Entity, IAggregateRoot
{
    public string FinishedProductReceiptId { get; private set; }
    public DateTime Timestamp { get; private set; }

    public int EmployeeId { get; private set; }
    public Employee Employee { get; private set; }
    public List<FinishedProductReceiptEntry> Entries { get; private set; }

    public FinishedProductReceipt(string finishedProductReceiptId, DateTime timestamp, int employeeId)
    {
        FinishedProductReceiptId = finishedProductReceiptId;
        Timestamp = timestamp;
        EmployeeId = employeeId;
        Entries = new List<FinishedProductReceiptEntry>();
    }
}
