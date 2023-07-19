namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

[DataContract]
public class CreateGoodsIssueCommand : IRequest<bool>
{
    [DataMember]
    public string GoodsIssueId { get; private set; }
    [DataMember]
    public string Receiver { get; private set; }
    [DataMember]
    public string EmployeeId { get; private set; }
    [DataMember]
    public List<CreateGoodsIssueEntryViewModel> Entries { get; private set; }
    public CreateGoodsIssueCommand(string goodsIssueId, string receiver, string employeeId, 
        List<CreateGoodsIssueEntryViewModel> entries)
    {
        GoodsIssueId = goodsIssueId;
        Receiver = receiver;
        EmployeeId = employeeId;
        Entries = entries;
    }
}
