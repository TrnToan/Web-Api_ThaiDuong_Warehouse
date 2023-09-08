namespace ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate;
public interface IItemRepository : IRepository<Item>
{
    Item Add(Item item);
    Item Update(Item item);
    Task<Item?> GetItemById(string itemId, string unit);
    Task<IEnumerable<Item>> GetItemsByItemClass(string? itemClassId); 
    Task<Item?> GetItemByEntityId(int Id);
}
