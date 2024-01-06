using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThaiDuongWarehouse.Domain.AggregateModels.ItemLotLocationAggregate;
public interface IItemLotLocationRepository : IRepository<ItemLotLocation>
{
    Task<ItemLotLocation?> GetByIdAsync(int itemLotId, int locationId);
    Task AddAsync(ItemLotLocation itemLotLocation);
    void Update(ItemLotLocation itemLotLocation);
}
