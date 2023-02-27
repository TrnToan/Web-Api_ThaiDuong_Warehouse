namespace ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate;
public class Item : Entity, IAggregateRoot
{
    public string ItemId { get; private set; }
    public string ItemName { get; private set; }
    public double MinimumStockLevel { get; private set; }
    public double Price { get; private set; }
    public Unit Unit { get; private set; }
    public ItemClass ItemClass { get; private set; }  

    public Item(string itemId, string itemName, Unit unit, ItemClass itemClass, double minimumStockLevel, 
        double price)
    {
        ItemId = itemId;
        ItemName = itemName;
        Unit = unit;
        ItemClass = itemClass;
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
