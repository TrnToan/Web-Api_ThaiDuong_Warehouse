namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class DepartmentRepository : BaseRepository, IDepartmentRepository
{
    public DepartmentRepository(WarehouseDbContext context) : base(context)
    {
    }

    public Department Add(Department department)
    {
        if (department.IsTransient())
        {
            return _context.Departments.Add(department).Entity;
        }
        else return department;
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _context.Departments.ToListAsync();
    }
}
