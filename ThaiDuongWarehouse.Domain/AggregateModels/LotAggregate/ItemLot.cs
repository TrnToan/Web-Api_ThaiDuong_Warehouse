﻿namespace ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate;
public class ItemLot : Entity, IAggregateRoot
{
    public string LotId { get; private set; }   
    public double Quantity { get; private set; }
    public DateTime Timestamp { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public bool IsIsolated { get; private set; } = false;
    public int ItemId { get; private set; }                 // ForeignKey
    public List<Location>? Locations { get; private set; }
    public Item Item { get; private set; }

    public ItemLot(string lotId, int itemId, double quantity, DateTime timestamp,
        DateTime? productionDate, DateTime? expirationDate)
    {
        LotId = lotId;
        ItemId = itemId;
        Quantity = quantity;
        Timestamp = timestamp;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
    }

    public ItemLot(string lotId, double quantity, DateTime timestamp, int itemId)
    {
        LotId = lotId;
        Quantity = quantity;
        Timestamp = timestamp;
        ItemId = itemId;
    }

    public void SetQuantity(double quantity)
    {
        Quantity = quantity;
    }
    public void Update(double quantity)
    {
        Quantity = quantity;
    }
    public void UpdateExistedLot(string lotId, List<Location>? locations, double quantity, DateTime? productionDate, DateTime? expirationDate)
    {
        // Mã lô có thể là mã cũ hoặc mã mới (nếu người dùng thay đổi mã)
        LotId = lotId;
        // Cập nhật lại các vị trí của lô nếu người dùng thay đổi thông tin 
        if (locations is not null)
            Locations = locations;

        Quantity = quantity;

        if (productionDate is not null)
            ProductionDate = productionDate;

        if (expirationDate is not null)
            ExpirationDate = expirationDate;
    }
    public void UpdateState(bool isIsolated)
    {
        IsIsolated = isIsolated;
    }
    public static void Reject(ItemLot lot)
    {
        //lot.AddDomainEvent(new InventoryLogEntryChangedDomainEvent(lot.LotId, -lot.Quantity, lot.ItemId, DateTime.Now));
    }
}
