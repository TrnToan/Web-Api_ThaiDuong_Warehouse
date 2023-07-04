namespace ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate;
public class ItemLot : Entity, IAggregateRoot
{
    public string LotId { get; private set; }   
    public double Quantity { get; private set; }
    public DateTime Timestamp { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public bool IsIsolated { get; private set; } = false;
    public int? LocationId { get; private set; }             // ForeignKey
    public int ItemId { get; private set; }                 // ForeignKey
    public Location? Location { get; private set; }
    public Item Item { get; private set; }

    public ItemLot(string lotId, int? locationId, int itemId, double quantity, DateTime timestamp,
        DateTime? productionDate, DateTime? expirationDate)
    {
        LotId = lotId;
        LocationId = locationId;
        ItemId = itemId;
        Quantity = quantity;
        Timestamp = timestamp;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
    }

    public ItemLot(string lotId, double quantity, DateTime timestamp, int itemId)
    {
        LotId = lotId;
        Quantity = quantity;
        Timestamp = timestamp;
        ItemId = itemId;
    }

    public void SetQuantity(double quantity)
    {
        Quantity = quantity;
    }
    public void Update(double quantity)
    {
        Quantity = quantity;
    }
    public void UpdateExistedLot(string lotId, int? locationId, double quantity, DateTime? productionDate, DateTime? expirationDate)
    {
        LotId = lotId;
        LocationId = locationId;
        Quantity = quantity;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
    }
    public void UpdateState(bool isIsolated)
    {
        IsIsolated = isIsolated;
    }
    public static void Reject(ItemLot lot)
    {
        //lot.AddDomainEvent(new InventoryLogEntryChangedDomainEvent(lot.LotId, -lot.Quantity, lot.ItemId, DateTime.Now));
    }
}
