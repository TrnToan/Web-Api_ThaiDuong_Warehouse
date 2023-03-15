using Microsoft.EntityFrameworkCore.Storage;
using ThaiDuongWarehouse.Infrastructure.EntityConfigurations;

namespace ThaiDuongWarehouse.Infrastructure;
public class WarehouseDbContext : DbContext, IUnitOfWork
{
    public DbSet<LotAdjustment> LotAdjustments { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<GoodsIssue> GoodsIssues { get; set; }
    public DbSet<GoodsReceipt> GoodsReceipts { get; set; }
    public DbSet<InventoryLogEntry> InventoryLogEntries { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemLot> ItemLots { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }

    private IDbContextTransaction? _currentTransaction;
    private readonly IMediator _mediator;

    public WarehouseDbContext(DbContextOptions options) : base(options) { }
    public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;
    public WarehouseDbContext(DbContextOptions options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LotAdjustmentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GoodsIssueEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GoodsReceiptEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryLogEntryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ItemLotEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WarehouseEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ItemClassEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UnitEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new LocationEntityTypeConfiguration());
    }
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }
    public async Task<IDbContextTransaction?> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync();

        return _currentTransaction;
    }
    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            transaction.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}
