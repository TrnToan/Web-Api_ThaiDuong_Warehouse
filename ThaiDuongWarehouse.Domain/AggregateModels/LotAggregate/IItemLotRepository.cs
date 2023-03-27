namespace ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate;
public interface IItemLotRepository : IRepository<ItemLot>
{
    void AddLot(ItemLot itemLot);
    void UpdateLot(ItemLot itemLot);
    Task<ItemLot?> GetLotByLotId(string lotId);
    Task<IEnumerable<ItemLot>> GetLotByItemId(string itemId);
    Task<IEnumerable<ItemLot>> GetLotByPO(string purchaseOrderNumber);
    Task<IEnumerable<ItemLot>> GetIsolatedItemLots();
    Task<IEnumerable<ItemLot>> GetAll();
}
