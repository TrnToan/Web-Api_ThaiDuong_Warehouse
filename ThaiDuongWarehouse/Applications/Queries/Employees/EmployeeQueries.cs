namespace ThaiDuongWarehouse.Api.Applications.Queries.Employees;

public class EmployeeQueries : IEmployeeQueries
{
    private readonly WarehouseDbContext _dbContext;
    private readonly IMapper _mapper;
    public EmployeeQueries(WarehouseDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeViewModel>> GetAllEmployee()
    {
        var employees = await _dbContext.Employees.ToListAsync();
        var viewmodels = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
        return viewmodels;
    }

    public async Task<EmployeeViewModel?> GetEmployeeById(string employeeId)
    {
        var employee = await _dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        if (employee is null) return null;

        var viewmodel = _mapper.Map<Employee, EmployeeViewModel>(employee);
        return viewmodel;
    }
}
