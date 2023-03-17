using ThaiDuongWarehouse.Domain.AggregateModels.AdjustmentAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Queries.LotAdjustments;

public class LotAdjustmentQueries : ILotAdjustmentQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;
    public LotAdjustmentQueries(WarehouseDbContext context ,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<LotAdjustmentViewModel>> GetAll()
    {
        var adjustments = await _context.LotAdjustments.ToListAsync();
        var viewmodels = _mapper.Map<IEnumerable<LotAdjustment>, IEnumerable<LotAdjustmentViewModel>>(adjustments);

        return viewmodels;
    }

    public async Task<IEnumerable<LotAdjustmentViewModel>> GetUnconfirmedAdjustments()
    {
        var adjustments = await _context.LotAdjustments
            .Where(la => la.IsConfirmed == false)
            .ToListAsync();
        var viewmodels = _mapper.Map<IEnumerable<LotAdjustment>, IEnumerable<LotAdjustmentViewModel>>(adjustments);

        return viewmodels;
    }
}
