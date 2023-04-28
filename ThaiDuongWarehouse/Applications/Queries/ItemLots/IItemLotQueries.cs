namespace ThaiDuongWarehouse.Api.Applications.Queries.ItemLots;

public interface IItemLotQueries
{
    Task<IEnumerable<ItemLotViewModel>> GetItemLotsByItemId(string itemId);
    Task<ItemLotViewModel> GetItemLotByLotId(string lotId);
    Task<IEnumerable<ItemLotViewModel>> GetItemLotsByPO(string purchaseOrderNumber);
    Task<IEnumerable<ItemLotViewModel>> GetIsolatedItemLots();
    Task<IList<ItemLotViewModel>> GetItemLotsByLocationId(string locationId);
}
