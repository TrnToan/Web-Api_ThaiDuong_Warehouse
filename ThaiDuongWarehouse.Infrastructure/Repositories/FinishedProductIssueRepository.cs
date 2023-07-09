namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class FinishedProductIssueRepository : BaseRepository, IFinishedProductIssueRepository
{
    public FinishedProductIssueRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<FinishedProductIssue?> GetIssueById(string id)
    {
        return await _context.FinisedProductIssues
            .Include(gi => gi.Employee)
            .Include(gi => gi.Entries)
                .ThenInclude(gi => gi.Item)
            .FirstOrDefaultAsync(gi => gi.FinishedProductIssueId == id);
    }
}
