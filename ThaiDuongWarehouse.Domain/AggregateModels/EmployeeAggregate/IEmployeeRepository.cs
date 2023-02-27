namespace ThaiDuongWarehouse.Domain.AggregateModels.EmployeeAggregate;
public interface IEmployeeRepository : IRepository<Employee>
{
    void Add(Employee employee);
    void Update(Employee employee);
    Task<Employee?> GetEmployeeById(string employeeId);
    Task<IEnumerable<Employee>> GetAllAsync();
}
