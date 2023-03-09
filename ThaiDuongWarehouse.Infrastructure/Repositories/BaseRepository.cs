namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public abstract class BaseRepository
{
    protected readonly WarehouseDbContext _context;
    public IUnitOfWork UnitOfWork
    {
        get
        {
            return (IUnitOfWork)_context;
        }
    }

    public BaseRepository(WarehouseDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}
