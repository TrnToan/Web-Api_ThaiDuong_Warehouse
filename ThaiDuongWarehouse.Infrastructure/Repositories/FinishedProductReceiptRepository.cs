namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class FinishedProductReceiptRepository : BaseRepository, IFinishedProductReceiptRepository
{
    public FinishedProductReceiptRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<FinishedProductReceipt> Add(FinishedProductReceipt finishedProductReceipt)
    {
        if (finishedProductReceipt.IsTransient())
        {
            var entry = await _context.FinishedProductReceipts
                .AddAsync(finishedProductReceipt);

            return entry.Entity;
        }           
        else
            throw new DbUpdateException("Unable to add finishedProductReceipt to Database.");
    }

    public async Task<FinishedProductReceipt?> GetReceiptById(string finishedProductReceiptId)
    {
        return await _context.FinishedProductReceipts
            .Include(gr => gr.Employee)
            .Include(gr => gr.Entries)
            .FirstOrDefaultAsync(gr => gr.FinishedProductReceiptId == finishedProductReceiptId);
    }

    public void Update(FinishedProductReceipt finishedProductReceipt)
    {
        _context.FinishedProductReceipts.Update(finishedProductReceipt);
    }
}
