using ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate;

namespace ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductInventoryAggregate;
public interface IFinishedProductInventoryRepository
{
    Task<FinishedProductInventory?> GetFinishedProductInventory(string itemId, string unit, string PO, DateTime timestamp);
    Task<FinishedProductInventory> Add(FinishedProductInventory finishedProductInventory); 
    void Update(FinishedProductInventory finishedProductInventory); 
    void Remove(FinishedProductInventory finishedProductInventory);
}
