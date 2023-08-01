namespace ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate;
public class ItemLot : Entity, IAggregateRoot
{
    public string LotId { get; private set; }   
    public double Quantity { get; private set; }
    public DateTime Timestamp { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public bool IsIsolated { get; private set; } = false;
    public int ItemId { get; private set; }                 // ForeignKey
    public List<ItemLotLocation> ItemLotLocations { get; private set; }
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
        ItemLotLocations = new List<ItemLotLocation>();
    }

    public void SetQuantity(double quantity)
    {
        Quantity = quantity;
    }
    public void Update(double changedQuantity)
    {
        Quantity += changedQuantity;
    }
    public void UpdateExistedLot(string lotId, List<ItemLotLocation>? itemLotLocations, double quantity, DateTime? productionDate, DateTime? expirationDate)
    {
        // Mã lô có thể là mã cũ hoặc mã mới (nếu người dùng thay đổi mã)
        LotId = lotId;
        // Cập nhật lại các vị trí của lô nếu người dùng thay đổi thông tin 
        if (itemLotLocations is not null)
            ItemLotLocations = itemLotLocations;

        Quantity += quantity;

        if (productionDate is not null)
            ProductionDate = productionDate;

        if (expirationDate is not null)
            ExpirationDate = expirationDate;
    }
    public void UpdateState(bool isIsolated)
    {
        IsIsolated = isIsolated;
    }

    public void RemoveItemLotLocation (ItemLotLocation itemLotLocation)
    {
        ItemLotLocations?.Remove(itemLotLocation);
    }

    public void UpdateItemLotLocation (int itemLotId, int locationId, double quantityPerLocation)
    {
        var subItemLot = ItemLotLocations?.Find(ill => ill.ItemLotId == itemLotId && ill.LocationId == locationId);
        if (subItemLot == null)
        {
            throw new WarehouseDomainException($"ItemLotLocation not found.");
        }
        subItemLot.UpdateQuantity(quantityPerLocation);
    }
    public static void Reject(ItemLot lot)
    {
        // Method intentionally left empty.
    }
}
