namespace ThaiDuongWarehouse.Domain.DomainEvents.ItemLotEvents;
public class IsolateItemLotsDomainEvent :  INotification
{
    public string ItemLotId { get; private set; }
    public double Quantity { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public int ItemId { get; private set; }

    public IsolateItemLotsDomainEvent(string itemLotId, double quantity, DateTime? productionDate, DateTime? expirationDate, int itemId)
    {
        ItemLotId = itemLotId;
        Quantity = quantity;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        ItemId = itemId;
    }
}
