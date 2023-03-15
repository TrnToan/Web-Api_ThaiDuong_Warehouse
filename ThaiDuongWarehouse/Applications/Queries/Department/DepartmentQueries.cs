using Department = ThaiDuongWarehouse.Domain.AggregateModels.DepartmentAggregate.Department;
namespace ThaiDuongWarehouse.Api.Applications.Queries.Department;

public class DepartmentQueries : IDepartmentQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;
    public DepartmentQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DepartmentViewModel>> GetAllDepartments()
    {
        var departments = await _context.Departments.ToListAsync();
        var viewmodels = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);

        return viewmodels;
    }
}
