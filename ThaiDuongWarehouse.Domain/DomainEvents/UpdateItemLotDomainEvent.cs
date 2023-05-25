namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class UpdateItemLotDomainEvent : INotification
{
    public string ItemLotId { get; private set; }
    public int LocationId { get; private set; }             
    public int ItemId { get; private set; }                 
    public double Quantity { get; private set; }
    public string Unit { get; private set; }
    public double? SublotSize { get; private set; }
    public string? SublotUnit { get; private set; }
    public string? PurchaseOrderNumber { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }

    public UpdateItemLotDomainEvent(string itemLotId, int locationId, int itemId, double quantity, string unit, 
        double? sublotSize, string? sublotUnit, string? purchaseOrderNumber, DateTime? productionDate, DateTime? expirationDate)
    {
        ItemLotId = itemLotId;
        LocationId = locationId;
        ItemId = itemId;
        Quantity = quantity;
        Unit = unit;
        SublotSize = sublotSize;
        SublotUnit = sublotUnit;
        PurchaseOrderNumber = purchaseOrderNumber;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
    }
}
