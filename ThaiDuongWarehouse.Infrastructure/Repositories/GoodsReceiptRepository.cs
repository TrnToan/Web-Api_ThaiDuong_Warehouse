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
            return goodsReceipt;
    }

    public async Task<GoodsReceipt?> GetGoodsReceiptByGoodsReceiptId(string goodsReceiptId)
    {
        return await _context.GoodsReceipts
            .Include(gr => gr.Lots)
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

    public async Task<GoodsReceiptLot?> GetGoodsReceiptLotById(string goodsReceiptLotId)
    {
        return await _context.GoodsReceipts
            .AsNoTracking()
            .SelectMany(gr => gr.Lots)
            .FirstOrDefaultAsync(lot => lot.GoodsReceiptLotId == goodsReceiptLotId);
    }

    public async Task<GoodsReceipt?> GetGoodsReceiptById(int id)
    {
        return await _context.GoodsReceipts
            .FirstOrDefaultAsync(gr => gr.Id == id);
    }
}
