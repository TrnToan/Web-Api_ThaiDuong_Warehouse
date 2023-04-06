namespace ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate;
public interface IItemLotRepository : IRepository<ItemLot>
{
    void AddLot(ItemLot itemLot);
    void Addlots(IEnumerable<ItemLot> itemLots);
    void UpdateLot(ItemLot itemLot);
    void RemoveLots(IEnumerable<ItemLot> itemLots);
    Task<ItemLot?> GetLotByLotId(string lotId);
    Task<IEnumerable<ItemLot>> GetLotsByItemId(string itemId, string unit);
    Task<IEnumerable<ItemLot>> GetLotByPO(string purchaseOrderNumber);
    Task<IEnumerable<ItemLot>> GetIsolatedItemLots();
    Task<IEnumerable<ItemLot>> GetAll();
}
