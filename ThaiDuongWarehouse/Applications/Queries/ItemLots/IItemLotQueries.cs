namespace ThaiDuongWarehouse.Api.Applications.Queries.ItemLots;

public interface IItemLotQueries
{
    Task<IEnumerable<ItemLotViewModel>> GetAll();
    Task<IEnumerable<ItemLotViewModel>> GetItemLots(string itemId);
    Task<IEnumerable<ItemLotViewModel>> GetItemLotsByItemId(DateTime timestamp, string itemId);
    Task<ItemLotViewModel> GetItemLotByLotId(string lotId);
    Task<IEnumerable<ItemLotViewModel>> GetIsolatedItemLots();
    Task<IEnumerable<ItemLotViewModel>> GetItemLotsByLocation(string locationId);
}
