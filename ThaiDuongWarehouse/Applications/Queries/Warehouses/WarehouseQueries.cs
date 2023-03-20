namespace ThaiDuongWarehouse.Api.Applications.Queries.Warehouses;

public class WarehouseQueries : IWarehouseQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;
    public WarehouseQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WarehouseViewModel>> GetAllWarehouses()
    {
        var warehouses = await _context.Warehouses
            .Include(w => w.Locations)
            .ToListAsync();
        var viewmodels = _mapper.Map<IEnumerable<Warehouse>, IEnumerable<WarehouseViewModel>>(warehouses);

        return viewmodels;
    }

    public async Task<WarehouseViewModel?> GetWarehouseById(string warehouseId)
    {
        var warehouse = await _context.Warehouses
            .Include(w => w.Locations)
            .FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);
        var viewmodel = _mapper.Map<Warehouse?, WarehouseViewModel>(warehouse);

        return viewmodel;
    }
}
