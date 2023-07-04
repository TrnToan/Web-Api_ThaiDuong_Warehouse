namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class UpdateItemLotDomainEvent : INotification
{
    public string OldItemLotId { get; private set; }
    public string? NewItemLotId { get; private set; }
    public Location? Location { get; private set; }             
    public double Quantity { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }

    public UpdateItemLotDomainEvent(string oldItemLotId, string? newItemLotId, Location? location, double quantity, 
        DateTime? productionDate, DateTime? expirationDate)
    {
        OldItemLotId = oldItemLotId;
        NewItemLotId = newItemLotId;
        Location = location;
        Quantity = quantity;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
    }
}
