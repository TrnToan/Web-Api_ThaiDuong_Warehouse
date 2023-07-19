namespace ThaiDuongWarehouse.Api.Applications.Queries.ItemLots;

public class ItemLotViewModel
{
    public string LotId { get; private set; }
    public bool IsIsolated { get; private set; } 
    public double Quantity { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public List<ItemSublotViewModel>? ItemLotLocations { get; private set; }
    public ItemViewModel Item { get; private set; }
    public ItemLotViewModel(string lotId, bool isIsolated, double quantity, DateTime? productionDate, DateTime? expirationDate, 
        List<ItemSublotViewModel>? itemLotLocations, ItemViewModel item)
    {
        LotId = lotId;
        IsIsolated = isIsolated;
        Quantity = quantity;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        ItemLotLocations = itemLotLocations;
        Item = item;
    }
    public ItemLotViewModel()
    {
    }
}
