namespace ThaiDuongWarehouse.Api.Applications.Queries.Items;

public interface IItemQueries
{
    Task<IEnumerable<ItemViewModel>> GetAllItemsAsync(string? itemClassId);
    Task<ItemViewModel?> GetItemByIdAsync(string itemId, string unit);
}
