namespace ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;
public class InventoryLogEntry : Entity, IAggregateRoot
{
    public Item Item { get; private set; }
    public DateTime Timestamp { get; private set; }
    public double BeforeQuantity { get; private set; }
    public double ChangedQuantity { get; private set; }
    public List<ItemLot> ItemLots { get; private set; }

    public InventoryLogEntry(Item item, DateTime timestamp, double beforeQuantity, double changedQuantity)
    {
        Item = item;
        Timestamp = timestamp;
        BeforeQuantity = beforeQuantity;
        ChangedQuantity = changedQuantity;
        ItemLots = new List<ItemLot>();
    }
}
