namespace ThaiDuongWarehouse.Api.Applications.Queries.ItemLots;

public interface IItemLotQueries
{
    Task<IEnumerable<ItemLotViewModel>> GetAll();
    Task<IEnumerable<ItemLotViewModel>> GetItemLotsByItemId(DateTime timestamp, string itemId);
    Task<ItemLotViewModel> GetItemLotByLotId(string lotId);
    Task<IEnumerable<ItemLotViewModel>> GetIsolatedItemLots();
}
