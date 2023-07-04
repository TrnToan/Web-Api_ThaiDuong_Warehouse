namespace ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;
public class InventoryLogEntry : Entity, IAggregateRoot
{
    public int ItemId { get; private set; }
    public string? ItemLotId { get; private set; }
    public double BeforeQuantity { get; private set; }
    public double ChangedQuantity { get; private set; }
    public double ReceivedQuantity { get; private set; }
    public double ShippedQuantity { get; private set; }
    public DateTime Timestamp { get; private set; }
    public Item Item { get; private set; }

    public InventoryLogEntry(int itemId, string? itemLotId, DateTime timestamp, double beforeQuantity, double changedQuantity,
        double receivedQuantity, double shippedQuantity)
    {
        ItemId = itemId;
        ItemLotId = itemLotId;
        Timestamp = timestamp;
        BeforeQuantity = beforeQuantity;
        ChangedQuantity = changedQuantity;
        ReceivedQuantity = receivedQuantity;
        ShippedQuantity = shippedQuantity;
    }

    public InventoryLogEntry(DateTime timestamp, string? itemLotId, double beforeQuantity, double changedQuantity, Item item)
    {
        Timestamp = timestamp;
        ItemLotId = itemLotId;
        BeforeQuantity = beforeQuantity;
        ChangedQuantity = changedQuantity;
        Item = item;
    }

    public void ModifyLogEntry(string newLotId)
    {
        ItemLotId = newLotId;
    }

    public void UpdateQuantity(double quantity)
    {
        ChangedQuantity = quantity;
    }

    public void UpdateEntry(double beforeQuantity, double changedQuantity)
    {
        BeforeQuantity = beforeQuantity + changedQuantity;
    }
}
