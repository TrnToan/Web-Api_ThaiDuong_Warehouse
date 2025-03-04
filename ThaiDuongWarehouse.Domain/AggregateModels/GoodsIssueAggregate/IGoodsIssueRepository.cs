﻿namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;
public interface IGoodsIssueRepository : IRepository<GoodsIssue>
{
    GoodsIssue Add(GoodsIssue goodsIssue);
    GoodsIssue Update(GoodsIssue goodsIssue);
    Task<IEnumerable<GoodsIssue>> GetListAsync(DateTime startTime, DateTime endTime);
    Task<GoodsIssue?> GetGoodsIssueById(string id);
    Task<GoodsIssueLot?> GetGoodsIssueLotById(string lotId);
    void Remove(GoodsIssue goodsIssue);
}
