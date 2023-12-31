namespace ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate;
public interface IItemLotRepository : IRepository<ItemLot>
{
    void AddLot(ItemLot itemLot);
    void Addlots(IEnumerable<ItemLot> itemLots);
    void UpdateLot(ItemLot itemLot);
    void UpdateLots(IEnumerable<ItemLot> itemLots);
    void RemoveLots(IEnumerable<ItemLot> itemLots);
    void RemoveLot(ItemLot itemLot);
    Task<ItemLot?> GetLotByLotId(string lotId);
    Task<IEnumerable<ItemLot>> GetLotsByItemId(string itemId, string unit);
    Task<IEnumerable<ItemLot>> GetIsolatedItemLots();
}
