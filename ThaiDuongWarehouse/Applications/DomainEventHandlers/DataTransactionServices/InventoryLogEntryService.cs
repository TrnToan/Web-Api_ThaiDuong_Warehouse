using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

namespace ThaiDuongWarehouse.Api.Applications.DomainEventHandlers.DataTransactionServices;

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

    public InventoryLogEntry? FindLatestServiceEntry(int itemId)
    {
        return Entries.LastOrDefault(e => e.ItemId == itemId);
    }

    public InventoryLogEntry? GetLogEntry(int itemId, string lotId, DateTime timestamp)
    {
        return Entries.Find(log => log.ItemId == itemId && log.ItemLotId == lotId && log.Timestamp == timestamp);
    }

    public InventoryLogEntry? GetFinishedProductLogEntry(int itemId, string PO, DateTime timestamp)
    {
        return Entries.Find(log => log.ItemId == itemId && log.ItemLotId == PO && log.Timestamp == timestamp);
    }


    public List<InventoryLogEntry> GetLogEntries(int itemId, DateTime timestamp)
    {
        return Entries
            .Where(log => log.ItemId == itemId)
            .Where(log => log.TrackingTime >= timestamp)
            .ToList();
    }

#pragma warning disable CS1998
    public async Task<InventoryLogEntry?> GetPreviousLogEntry(int itemId, DateTime trackingTime)
    {
        return Entries
            .Where(log => log.TrackingTime < trackingTime)
            .OrderBy(log => log.TrackingTime)
            .LastOrDefault(log => log.ItemId == itemId);
    }

    public async Task<List<InventoryLogEntry>> GetLatestLogEntries(int itemId, DateTime timestamp)
    {
        return Entries
            .Where(log => log.ItemId == itemId)
            .Where(log => log.TrackingTime >= timestamp)
            .ToList();
    }
}
