namespace ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

public class DeleteFinishedProductReceiptCommand : IRequest<bool>
{
    public string FinishedProductReceiptId { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public DeleteFinishedProductReceiptCommand()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {    }
    public DeleteFinishedProductReceiptCommand(string finishedProductReceiptId)
    {
        FinishedProductReceiptId = finishedProductReceiptId;
    }
}
