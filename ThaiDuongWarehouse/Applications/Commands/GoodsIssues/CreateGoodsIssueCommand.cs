namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

[DataContract]
public class CreateGoodsIssueCommand : IRequest<bool>
{
    [DataMember]
    public string GoodsIssueId { get; private set; }
    [DataMember]
    public string Receiver { get; private set; }
    [DataMember]
    public DateTime Timestamp { get; private set; }
    [DataMember]
    public string EmployeeId { get; private set; }
    [DataMember]
    public List<CreateGoodsIssueEntryViewModel> Entries { get; private set; }
    public CreateGoodsIssueCommand(string goodsIssueId, string receiver,
        DateTime timestamp, string employeeId, List<CreateGoodsIssueEntryViewModel> entries)
    {
        GoodsIssueId = goodsIssueId;
        Receiver = receiver;
        Timestamp = timestamp;
        EmployeeId = employeeId;
        Entries = entries;
    }
}
