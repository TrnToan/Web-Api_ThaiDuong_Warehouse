namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    public EmployeeRepository(WarehouseDbContext context) : base(context)
    {
    }

    public Employee Add(Employee employee)
    {
        if (employee.IsTransient())
        {
            return _context.Employees.Add(employee).Entity;
        }
        else return employee;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees.ToListAsync();
        // Khi dùng tới lệnh ToListAsync Database gọi tới Queries
    }

    public async Task<Employee?> GetEmployeeById(string employeeId)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
    }

    public Employee Update(Employee employee)
    {
        return _context.Employees.Update(employee).Entity;
    }
}
