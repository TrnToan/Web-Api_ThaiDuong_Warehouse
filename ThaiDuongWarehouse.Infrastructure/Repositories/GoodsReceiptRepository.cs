namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class GoodsReceiptRepository : BaseRepository, IGoodsReceiptRepository
{
    public GoodsReceiptRepository(WarehouseDbContext context) : base(context)
    {
    }

    public GoodsReceipt Add(GoodsReceipt goodsReceipt)
    {
        if (goodsReceipt.IsTransient())
            return _context.GoodsReceipts
                .Add(goodsReceipt).Entity;
        else
            throw new Exception("Failed to add goodsreceipt.");
    }

    public async Task<IEnumerable<GoodsReceipt>> GetConfirmedGoodsReceipt()
    {
        return await _context.GoodsReceipts
            .Where(gr => gr.IsConfirmed == true)
            .ToListAsync();
    }

    public async Task<IEnumerable<GoodsReceipt>> GetUnConfirmedGoodsReceipt()
    {
        return await _context.GoodsReceipts
            .Where(gr => gr.IsConfirmed == false)
            .ToListAsync();
    }

    public async Task<GoodsReceipt?> GetGoodsReceiptById(string goodsReceiptId)
    {
        return await _context.GoodsReceipts
            .FirstOrDefaultAsync(gr => gr.GoodsReceiptId == goodsReceiptId);
    }

    public void Update(GoodsReceipt goodsReceipt)
    {
        _context.GoodsReceipts.Update(goodsReceipt);
    }

    public void Remove(GoodsReceipt goodsReceipt)
    {
        _context.GoodsReceipts.Remove(goodsReceipt);
    }
}
