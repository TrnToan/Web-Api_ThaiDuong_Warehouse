namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    public EmployeeRepository(WarehouseDbContext context) : base(context)
    {
    }

    public IUnitOfWork unitOfWork => throw new NotImplementedException();

    public void Add(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Employee>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Employee?> GetEmployeeById(string employeeId)
    {
        throw new NotImplementedException();
    }

    public void Update(Employee employee)
    {
        throw new NotImplementedException();
    }
}
