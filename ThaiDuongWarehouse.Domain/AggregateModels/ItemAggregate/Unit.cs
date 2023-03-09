namespace ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate;
public class Unit
{
    public string UnitName { get; private set; }
    public Unit(string unitName) 
    {
        UnitName = unitName;
    }
}
