namespace ThaiDuongWarehouse.Domain.AggregateModels.EmployeeAggregate;
public interface IEmployeeRepository : IRepository<Employee>
{
    Employee Add(Employee employee);
    Employee Update(Employee employee);
    Task<Employee?> GetEmployeeById(string employeeId);
    Task<IEnumerable<Employee>> GetAllAsync();
}
