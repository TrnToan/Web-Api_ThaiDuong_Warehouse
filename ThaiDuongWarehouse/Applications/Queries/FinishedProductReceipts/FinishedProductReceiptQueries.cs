using Microsoft.EntityFrameworkCore;

namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductReceipts;

public class FinishedProductReceiptQueries : IFinishedProductReceiptQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;

    public FinishedProductReceiptQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FinishedProductReceiptViewModel?> GetReceiptById(string id)
    {
        var goodsReceipt = await _context.FinishedProductReceipts
            .Include(gr => gr.Employee)
            .Include(gr => gr.Entries)
                .ThenInclude(gr => gr.Item)
            .FirstOrDefaultAsync(gr => gr.FinishedProductReceiptId == id);

        return _mapper.Map<FinishedProductReceiptViewModel>(goodsReceipt);
    }

    public async Task<IEnumerable<FinishedProductReceiptViewModel>> GetReceiptsAsync(TimeRangeQuery query)
    {
        var goodsReceipts = await _context.FinishedProductReceipts
            .Include(gr => gr.Employee)
            .Include(gr => gr.Entries)
                .ThenInclude(gr => gr.Item)
            .Where(gr => gr.Timestamp >= query.StartTime && gr.Timestamp <= query.EndTime)
            .Skip((query.Page - 1) * query.ItemsPerPage)
            .Take(query.ItemsPerPage)
            .ToListAsync();

        return _mapper.Map<IEnumerable<FinishedProductReceipt>, IEnumerable<FinishedProductReceiptViewModel>>(goodsReceipts);
    }
}
