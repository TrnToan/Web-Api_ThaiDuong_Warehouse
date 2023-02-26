namespace ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate
{
    public class Unit
    {
        public string Name { get; private set; }

        public Unit(string name)
        {
            Name = name;
        }
    }
}
