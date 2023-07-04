using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers;

public class InventoryLogEntryService
{
	public List<InventoryLogEntry> Entries { get; private set; }
	public InventoryLogEntryService()
	{
		Entries = new();
	}

    public void AddEntry(InventoryLogEntry entry)
    {
        Entries.Add(entry);
    }

    public void AddEntries(IEnumerable<InventoryLogEntry> entries)
	{
		Entries.AddRange(entries);
	}

	public InventoryLogEntry? FindEntry(int itemId)
	{
		return Entries.LastOrDefault(e => e.ItemId == itemId);
	}
	
	public InventoryLogEntry? GetLogEntry(string lotId, DateTime timestamp)
	{
		return Entries.FirstOrDefault(log => log.ItemLotId == lotId && log.Timestamp == timestamp);
	}

	public List<InventoryLogEntry> GetLogEntries(int itemId, DateTime timestamp) 
	{
		return Entries
			.Where(log => log.ItemId == itemId)
			.Where(log => log.Timestamp >= timestamp)
			.ToList();
	}
}
