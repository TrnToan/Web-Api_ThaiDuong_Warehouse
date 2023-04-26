﻿namespace ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate;
public class Item : Entity, IAggregateRoot
{
    public string ItemId { get; private set; }
    public string ItemClassId { get; private set; }             // ForeignKey
    public string Unit { get; private set; }                
    public string ItemName { get; private set; }
    public double MinimumStockLevel { get; private set; }
    public decimal? Price { get; private set; }
    public ItemClass ItemClass { get; private set; }

    public Item(string itemId, string itemClassId, string unit, string itemName,
        double minimumStockLevel, decimal? price)
    {
        ItemId = itemId;
        ItemClassId = itemClassId;
        Unit = unit;
        ItemName = itemName;
        MinimumStockLevel = minimumStockLevel;
        Price = price;
    }

    public Item(string itemId, string itemClassId, string unit)
    {
        ItemId = itemId;
        ItemClassId = itemClassId;
        Unit = unit;
        MinimumStockLevel = 0;
        Price = 0;
    }

    public Item()
    {
    }

    public void Update(string unit, double minimumStockLevel, decimal price)
    {
        Unit = unit;
        MinimumStockLevel = minimumStockLevel;
        Price = price;
    }

    public void CreateItemWithNewUnit(string itemId, string itemClassId, string unit)
    {
        this.AddDomainEvent(new CreateItemWithNewUnitDomainEvent(itemId, itemClassId, unit));
    }
}
