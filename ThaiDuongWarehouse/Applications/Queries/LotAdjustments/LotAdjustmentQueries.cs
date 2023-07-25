namespace ThaiDuongWarehouse.Api.Applications.Queries.LotAdjustments;

public class LotAdjustmentQueries : ILotAdjustmentQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;

    private IQueryable<LotAdjustment> _lotAdjustments => _context.LotAdjustments
        .AsNoTracking()
        .Include(a => a.Item)
        .Include(a => a.Employee)
        .Include(a => a.SublotAdjustments);

    public LotAdjustmentQueries(WarehouseDbContext context ,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LotAdjustmentViewModel>> GetAdjustmentsByTime(TimeRangeQuery query)
    {
        var adjustments = await _lotAdjustments
            .Where(la => la.Timestamp >= query.StartTime &&
            la.Timestamp <= query.EndTime)
            .Where(la => la.IsConfirmed)           
            .ToListAsync();

        return _mapper.Map<IEnumerable<LotAdjustment>, IEnumerable<LotAdjustmentViewModel>>(adjustments);
    }

    public async Task<IEnumerable<LotAdjustmentViewModel>> GetAll()
    {
        var adjustments = await _lotAdjustments
            .ToListAsync();
        var viewmodels = _mapper.Map<IEnumerable<LotAdjustment>, IEnumerable<LotAdjustmentViewModel>>(adjustments);
        
        return viewmodels;
    }

    public async Task<IEnumerable<LotAdjustmentViewModel>> GetIsConfirmedAdjustments(bool isConfirmed)
    {
        var adjustments = await _lotAdjustments
            .Where(la => la.IsConfirmed == isConfirmed)
            .ToListAsync();
        var viewmodels = _mapper.Map<IEnumerable<LotAdjustment>, IEnumerable<LotAdjustmentViewModel>>(adjustments);

        return viewmodels;
    }
}
