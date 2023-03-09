namespace ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;
public class InventoryLogEntry : Entity, IAggregateRoot
{
    public int ItemId { get; private set; }
    public int ItemLotId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public double BeforeQuantity { get; private set; }
    public double ChangedQuantity { get; private set; }
    public Item Item { get; private set; }

    public InventoryLogEntry(int itemId, int itemLotId, DateTime timestamp, double beforeQuantity, double changedQuantity)
    {
        ItemId = itemId;
        ItemLotId = itemLotId;
        Timestamp = timestamp;
        BeforeQuantity = beforeQuantity;
        ChangedQuantity = changedQuantity;
    }
}
