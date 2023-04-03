namespace ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;

public class InventoryLogEntryViewModel
{
    public string ItemLotId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public double BeforeQuantity { get; private set; }
    public double ChangedQuantity { get; private set; }
    public string Unit { get; private set; }
    public ItemViewModel Item { get; private set; }
    public InventoryLogEntryViewModel(string itemLotId, DateTime timestamp, double beforeQuantity, string unit,
        double changedQuantity, ItemViewModel item)
    {
        ItemLotId = itemLotId;
        Timestamp = timestamp;
        BeforeQuantity = beforeQuantity;
        ChangedQuantity = changedQuantity;
        Unit = unit;
        Item = item;
    }
}
