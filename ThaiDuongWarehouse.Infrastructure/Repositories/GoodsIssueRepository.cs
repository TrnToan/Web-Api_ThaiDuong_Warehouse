﻿namespace ThaiDuongWarehouse.Infrastructure.Repositories;
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
        return await _context.GoodsIssues.FirstOrDefaultAsync(gi => gi.GoodsIssueId == id);
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
