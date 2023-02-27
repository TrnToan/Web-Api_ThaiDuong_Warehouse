namespace ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate;
public class ItemClass
{
    public string ItemClassId { get; private set; }

    public ItemClass(string itemClassId)
    {
        ItemClassId = itemClassId;
    }
}
