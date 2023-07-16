namespace ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;

public class ItemLotLogEntryViewModel
{
    public string ItemLotId { get; private set; }
    public double Quantity { get; private set; }
    public ItemViewModel Item { get; private set; }

    public ItemLotLogEntryViewModel(string itemLotId, double quantity, ItemViewModel item)
    {
        ItemLotId = itemLotId;
        Quantity = quantity;
        Item = item;
    }
}
