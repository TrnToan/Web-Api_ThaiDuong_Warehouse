using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThaiDuongWarehouse.Domain.AggregateModels.IsolatedItemLotAggregate;
public interface IIsolatedItemLotRepository : IRepository<IsolatedItemLot>
{
    Task<IsolatedItemLot?> GetAsync(string itemLotId);
    Task AddAsync(IsolatedItemLot itemLot);
    void Update(IsolatedItemLot itemLot);
    void Remove(IsolatedItemLot itemLot);
}
