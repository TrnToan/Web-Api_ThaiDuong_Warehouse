namespace ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate;
public class Item : Entity, IAggregateRoot
{
    public string ItemId { get; private set; }
    public string ItemClassId { get; private set; }             // ForeignKey
    public string UnitName { get; private set; }                // ForeignKey
    public string ItemName { get; private set; }
    public double MinimumStockLevel { get; private set; }
    public double Price { get; private set; }
    public Unit Unit { get; private set; }
    public ItemClass ItemClass { get; private set; }

    public Item(string itemId, string itemClassId, string unitName, string itemName,
        double minimumStockLevel, double price)
    {
        ItemId = itemId;
        ItemClassId = itemClassId;
        UnitName = unitName;
        ItemName = itemName;
        MinimumStockLevel = minimumStockLevel;
        Price = price;
    }

    public void Update(Unit unit, double minimumStockLevel, double price)
    {
        Unit = unit;
        MinimumStockLevel = minimumStockLevel;
        Price = price;
    }
    public void ConfirmCreation()
    {
        AddDomainEvent(new ItemCreatedDomainEvent(this));
    }
}
