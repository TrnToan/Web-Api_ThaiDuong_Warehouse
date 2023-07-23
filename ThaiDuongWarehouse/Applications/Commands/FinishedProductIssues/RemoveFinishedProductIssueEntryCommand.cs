namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductIssues;

[DataContract]
public class RemoveFinishedProductIssueEntryCommand : IRequest<bool>
{
    [DataMember]
    public string FinishedProductIssueId { get; private set; }
    [DataMember]
    public string ItemId { get; private set; }
    [DataMember]
    public string Unit { get; private set; }
    [DataMember]
    public string PurchaseOrderNumber { get; private set; }

    public RemoveFinishedProductIssueEntryCommand(string finishedProductIssueId, string itemId, string unit, string purchaseOrderNumber)
    {
        FinishedProductIssueId = finishedProductIssueId;
        ItemId = itemId;
        Unit = unit;
        PurchaseOrderNumber = purchaseOrderNumber;
    }
}
