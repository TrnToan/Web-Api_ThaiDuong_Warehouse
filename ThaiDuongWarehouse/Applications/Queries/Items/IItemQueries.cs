namespace ThaiDuongWarehouse.Api.Applications.Queries.Items;

public interface IItemQueries
{
    Task<IEnumerable<ItemViewModel>> GetAllItemsAsync();
    Task<ItemViewModel?> GetItemByIdAsync(string ItemId, string unit);
}
