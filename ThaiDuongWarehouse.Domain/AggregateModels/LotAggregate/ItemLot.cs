namespace ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate;
public class ItemLot : Entity, IAggregateRoot
{
    public string LotId { get; private set; }
    public int LocationId { get; private set; }             // ForeignKey
    public int ItemId { get; private set; }                 // ForeignKey
    public bool IsIsolated { get; private set; } = false;
    public double Quantity { get; private set; }
    public string Unit { get; private set; }
    public double? SublotSize { get; private set; }
    public string? PurchaseOrderNumber { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public Location? Location { get; private set; }
    public Item Item { get; private set; }

    public ItemLot(string lotId, int locationId, int itemId, double quantity, string unit,
        double? sublotSize, string? purchaseOrderNumber, DateTime? productionDate, DateTime? expirationDate)
    {
        LotId = lotId;
        LocationId = locationId;
        ItemId = itemId;
        Quantity = quantity;
        Unit = unit;
        SublotSize = sublotSize;
        PurchaseOrderNumber = purchaseOrderNumber;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
    }
    public void Update(double quantity)
    {
        Quantity = quantity;
    }
    public void Update(double quantity, string purchaseOrderNumber)
    {
        Quantity = quantity;
        PurchaseOrderNumber = purchaseOrderNumber;
    }
}
