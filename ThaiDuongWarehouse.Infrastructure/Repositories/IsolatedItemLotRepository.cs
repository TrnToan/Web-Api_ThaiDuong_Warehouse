using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class IsolatedItemLotRepository : BaseRepository, IIsolatedItemLotRepository
{
    public IsolatedItemLotRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<IsolatedItemLot?> GetAsync(string itemLotId)
    {
        return await _context.IsolatedItemLots.FirstOrDefaultAsync(il => il.ItemLotId == itemLotId);
    }

    public async Task AddAsync(IsolatedItemLot itemLot)
    {
        await _context.IsolatedItemLots.AddAsync(itemLot);
    }

    public void Update(IsolatedItemLot itemLot)
    {
        _context.IsolatedItemLots.Update(itemLot);
    }

    public void Remove(IsolatedItemLot itemLot)
    {
        _context.IsolatedItemLots.Remove(itemLot);
    }
}
