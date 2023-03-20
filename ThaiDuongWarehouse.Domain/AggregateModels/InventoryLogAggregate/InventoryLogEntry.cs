namespace ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;
public class InventoryLogEntry : Entity, IAggregateRoot
{
    public int ItemId { get; private set; }
    public string ItemLotId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public double BeforeQuantity { get; private set; }
    public double ChangedQuantity { get; private set; }
    public Item Item { get; private set; }

    public InventoryLogEntry(int itemId, string itemLotId, DateTime timestamp, double beforeQuantity, double changedQuantity)
    {
        ItemId = itemId;
        ItemLotId = itemLotId;
        Timestamp = timestamp;
        BeforeQuantity = beforeQuantity;
        ChangedQuantity = changedQuantity;
    }

    public InventoryLogEntry(DateTime timestamp, string itemLotId, double beforeQuantity, double changedQuantity, Item item)
    {
        Timestamp = timestamp;
        ItemLotId = itemLotId;
        BeforeQuantity = beforeQuantity;
        ChangedQuantity = changedQuantity;
        Item = item;
    }
}
