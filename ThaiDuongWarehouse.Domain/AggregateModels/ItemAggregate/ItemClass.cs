namespace ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate;
public class ItemClass
{
    public string ItemClassId { get; private set; }
    public List<Item> Items { get; private set; } = new List<Item>();
    public ItemClass(string itemClassId) 
    {
        ItemClassId = itemClassId;
    }
}
