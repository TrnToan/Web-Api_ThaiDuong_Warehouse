namespace ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate;
public class Item : Entity, IAggregateRoot
{
    public string ItemId { get; private set; }
    public string ItemName { get; private set; }
    public string Unit { get; private set; }
    public string ItemClassId { get; private set; }              // ForeignKey
    public double MinimumStockLevel { get; private set; }
    public decimal? Price { get; private set; }
    public double? SublotSize { get; private set; }
    public string? PacketUnit { get; private set; }
    public ItemClass ItemClass { get; private set; }

    public Item(string itemId, string itemName, string unit, string itemClassId, double minimumStockLevel, decimal? price, 
        double? sublotSize, string? packetUnit) : this(itemId, itemName, unit, itemClassId, minimumStockLevel, price)
    {
        SublotSize = sublotSize;
        PacketUnit = packetUnit;
    }

    public Item(string itemId, string itemClassId, string unit, string itemName, double minimumStockLevel, decimal? price)
    {
        ItemId = itemId;
        ItemClassId = itemClassId;
        Unit = unit;
        ItemName = itemName;
        MinimumStockLevel = minimumStockLevel;
        Price = price;
    }

    public Item(string itemId, string itemName, string unit, string itemClassId) 
    { 
        ItemId = itemId;
        ItemClassId = itemClassId;
        Unit = unit;
        ItemName = itemName;
    }

    public void Update(string unit, double minimumStockLevel, decimal price)
    {
        Unit = unit;
        MinimumStockLevel = minimumStockLevel;
        Price = price;
    }
}
