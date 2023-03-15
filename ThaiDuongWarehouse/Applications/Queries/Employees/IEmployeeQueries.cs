namespace ThaiDuongWarehouse.Api.Applications.Queries.Employees;

public interface IEmployeeQueries
{
    Task<IEnumerable<EmployeeViewModel>> GetAllEmployee();
    Task<EmployeeViewModel?> GetEmployeeById(string employeeId);
}
