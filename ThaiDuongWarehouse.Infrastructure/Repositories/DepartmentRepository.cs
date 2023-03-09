namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class DepartmentRepository : BaseRepository, IDepartmentRepository
{
    public DepartmentRepository(WarehouseDbContext context) : base(context)
    {
    }

    public void Add(Department department)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Department>> GetAll()
    {
        throw new NotImplementedException();
    }
}
