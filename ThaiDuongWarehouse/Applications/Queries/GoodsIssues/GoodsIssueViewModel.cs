
namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsIssues;

public class GoodsIssueViewModel
{
    public string GoodsIssueId { get; private set; }
    public string Receiver { get; private set; }
    public string? PurchaseOrderNumber { get; private set; }
    public bool IsConfirmed { get; private set; }
    public DateTime Timestamp { get; private set; }
    public EmployeeViewModel Employee { get; private set; }
    public List<GoodsIssueEntryViewModel> Entries { get; private set; }
    public GoodsIssueViewModel(string goodsIssueId, string receiver, string? purchaseOrderNumber, 
        bool isConfirmed, DateTime timestamp, EmployeeViewModel employee, 
        List<GoodsIssueEntryViewModel> entries)
    {
        GoodsIssueId = goodsIssueId;
        Receiver = receiver;
        PurchaseOrderNumber = purchaseOrderNumber;
        IsConfirmed = isConfirmed;
        Timestamp = timestamp;
        Employee = employee;
        Entries = entries;
    }
}
