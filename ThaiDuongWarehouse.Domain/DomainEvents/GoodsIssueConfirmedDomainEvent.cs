namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class GoodsIssueConfirmedDomainEvent : INotification
{
    public bool IsConfirmed { get; private set; }
    public GoodsIssueConfirmedDomainEvent(bool isConfirmed)
    {
        IsConfirmed = isConfirmed;
    }
}
