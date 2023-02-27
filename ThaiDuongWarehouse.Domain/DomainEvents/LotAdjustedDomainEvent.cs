namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class LotAdjustedDomainEvent : INotification
{
    public bool IsConfirmed { get; private set; }
    public LotAdjustedDomainEvent(bool isConfirmed)
    {
        IsConfirmed = isConfirmed;
    }
}
