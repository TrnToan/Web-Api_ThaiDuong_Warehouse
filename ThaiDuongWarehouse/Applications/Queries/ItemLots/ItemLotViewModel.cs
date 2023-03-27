namespace ThaiDuongWarehouse.Api.Applications.Queries.ItemLots;

public class ItemLotViewModel
{
    public string LotId { get; private set; }
    public bool IsIsolated { get; private set; } 
    public double Quantity { get; private set; }
    public double? SublotSize { get; private set; }
    public string? PurchaseOrderNumber { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public LocationViewModel? Location { get; private set; }
    public ItemViewModel Item { get; private set; }
    public ItemLotViewModel(string lotId, bool isIsolated, double quantity, double? sublotSize, string? purchaseOrderNumber, 
        DateTime? productionDate, DateTime? expirationDate, LocationViewModel? location, ItemViewModel item)
    {
        LotId = lotId;
        IsIsolated = isIsolated;
        Quantity = quantity;
        SublotSize = sublotSize;
        PurchaseOrderNumber = purchaseOrderNumber;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        Location = location;
        Item = item;
    }
}
