namespace ThaiDuongWarehouse.Api.Applications.Queries.Department;

public interface IDepartmentQueries
{
    Task<IEnumerable<DepartmentViewModel>> GetAllDepartments();
}
