namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class GoodsIssueRepository : BaseRepository, IGoodsIssueRepository
{
    public GoodsIssueRepository(WarehouseDbContext context) : base(context)
    {
    }

    public IUnitOfWork unitOfWork => throw new NotImplementedException();

    public GoodsIssue Add(GoodsIssue goodsIssue)
    {
        throw new NotImplementedException();
    }

    public Task<GoodsIssue?> GetGoodsIssueById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GoodsIssue>> GetListAsync(DateTime startTime, DateTime endTime)
    {
        throw new NotImplementedException();
    }

    public GoodsIssue Update(GoodsIssue goodsIssue)
    {
        throw new NotImplementedException();
    }
}
