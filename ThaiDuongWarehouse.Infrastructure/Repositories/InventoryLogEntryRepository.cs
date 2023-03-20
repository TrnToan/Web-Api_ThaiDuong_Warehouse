﻿
using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class InventoryLogEntryRepository : BaseRepository, IInventoryLogEntryRepository
{
    public InventoryLogEntryRepository(WarehouseDbContext context) : base(context)
    {
    }

    public void Add(InventoryLogEntry logEntry)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<InventoryLogEntry>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<InventoryLogEntry>> GetByItem(string itemId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<InventoryLogEntry>> GetByTime(DateTime startTime, DateTime endTime)
    {
        throw new NotImplementedException();
    }
}
