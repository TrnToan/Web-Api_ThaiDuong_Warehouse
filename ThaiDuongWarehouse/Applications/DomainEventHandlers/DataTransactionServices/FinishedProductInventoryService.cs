using ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers.DataTransactionServices;

public class FinishedProductInventoryService
{
    public List<FinishedProductInventory> ProductInventoryService { get; private set; }
	public FinishedProductInventoryService()
	{
		ProductInventoryService = new();
	}

	public void Add(FinishedProductInventory finishedProductInventory)
	{
		ProductInventoryService.Add(finishedProductInventory);
	}
	
	public FinishedProductInventory? GetInventory(string itemId, string unit, string purchaseOrderNumber)
	{
		return ProductInventoryService
			.Find(p => p.Item.ItemId == itemId && p.Item.Unit == unit && p.PurchaseOrderNumber == purchaseOrderNumber);
	}
}
