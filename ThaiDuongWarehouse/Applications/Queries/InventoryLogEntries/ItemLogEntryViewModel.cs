namespace ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;

public class ItemLogEntryViewModel
{
    public ItemViewModel Item { get; private set; }
    public double TotalQuantity { get; private set; }
    public List<ItemLotLogEntryViewModel> Lots { get; private set; }

    public ItemLogEntryViewModel(ItemViewModel item, double totalQuantity, List<ItemLotLogEntryViewModel> lots)
    {
        Item = item;
        TotalQuantity = totalQuantity;
        Lots = lots;
    }
}
