using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class InventoryLogEntryService
{
    private List<InventoryLogEntry> _entries;
	public InventoryLogEntryService()
	{
		_entries = new();
	}
	
	public void AddEntry(InventoryLogEntry entry)
	{
		_entries.Add(entry);
	}

	public InventoryLogEntry? FindEntry(int itemId)
	{
		return _entries.LastOrDefault(e => e.ItemId == itemId);
	}
}
