namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class GoodsIssueRepository : BaseRepository, IGoodsIssueRepository
{
    public GoodsIssueRepository(WarehouseDbContext context) : base(context)
    {
    }

    public GoodsIssue Add(GoodsIssue goodsIssue)
    {
        return _context.GoodsIssues.Add(goodsIssue).Entity;
    }

    public async Task<GoodsIssue?> GetGoodsIssueById(string id)
    {
        return await _context.GoodsIssues
            .Include(g => g.Entries)
            .ThenInclude(g => g.Item)
            .FirstOrDefaultAsync(gi => gi.GoodsIssueId == id);
    }

    public async Task<GoodsIssueLot?> GetGoodsIssueLotById(string lotId)
    {
        return await _context.GoodsIssues
            .SelectMany(g => g.Entries
            .SelectMany(e => e.Lots))
            .FirstOrDefaultAsync(l => l.GoodsIssueLotId == lotId);
    }

    public Task<IEnumerable<GoodsIssue>> GetListAsync(DateTime startTime, DateTime endTime)
    {
        throw new NotImplementedException();
    }

    public GoodsIssue Update(GoodsIssue goodsIssue)
    {
        return _context.GoodsIssues.Update(goodsIssue).Entity;
    }
}
