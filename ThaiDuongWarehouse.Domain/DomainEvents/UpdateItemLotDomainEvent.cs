namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class UpdateItemLotDomainEvent : INotification
{
    public string OldItemLotId { get; private set; }
    public string? NewItemLotId { get; private set; }
    public List<Location>? Locations { get; private set; }             
    public double Quantity { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }

    public UpdateItemLotDomainEvent(string oldItemLotId, string? newItemLotId, List<Location>? locations, double quantity, 
        DateTime? productionDate, DateTime? expirationDate)
    {
        OldItemLotId = oldItemLotId;
        NewItemLotId = newItemLotId;
        Locations = locations;
        Quantity = quantity;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
    }
}
