namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class FinishedProductIssueRepository : BaseRepository, IFinishedProductIssueRepository
{
    public FinishedProductIssueRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<FinishedProductIssue> AddAsync(FinishedProductIssue finishedProductIssue)
    {
        if (finishedProductIssue.IsTransient())
        {
            var productIssue = await _context.FinisedProductIssues
                .AddAsync(finishedProductIssue);

            return productIssue.Entity;
        }
        else
            throw new DbUpdateException("Unable to add finishedProductReceipt to Database.");
    }

    public async Task<FinishedProductIssue?> GetIssueById(string id)
    {
        return await _context.FinisedProductIssues
            .Include(gi => gi.Employee)
            .Include(gi => gi.Entries)
                .ThenInclude(gi => gi.Item)
            .FirstOrDefaultAsync(gi => gi.FinishedProductIssueId == id);
    }

    public void Update(FinishedProductIssue finishedProductIssue)
    {
        _context.FinisedProductIssues.Update(finishedProductIssue);
    }
}
