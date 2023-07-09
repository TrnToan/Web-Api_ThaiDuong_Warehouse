namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class UpdateItemLotDomainEvent : INotification
{
    public string OldItemLotId { get; private set; }
    public string? NewItemLotId { get; private set; }
    public List<ItemLotLocation>? ItemLotLocations { get; private set; }             
    public double Quantity { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }

    public UpdateItemLotDomainEvent(string oldItemLotId, string? newItemLotId, List<ItemLotLocation>? itemLotLocations, 
        double quantity, DateTime? productionDate, DateTime? expirationDate)
    {
        OldItemLotId = oldItemLotId;
        NewItemLotId = newItemLotId;
        ItemLotLocations = itemLotLocations;
        Quantity = quantity;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
    }
}
