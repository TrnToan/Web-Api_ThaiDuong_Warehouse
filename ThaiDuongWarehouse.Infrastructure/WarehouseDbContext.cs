using Microsoft.EntityFrameworkCore.Storage;
using ThaiDuongWarehouse.Infrastructure.EntityConfigurations;

namespace ThaiDuongWarehouse.Infrastructure;
public class WarehouseDbContext : DbContext, IUnitOfWork
{
    public DbSet<LotAdjustment> LotAdjustments { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<GoodsIssue> GoodsIssues { get; set; }
    public DbSet<FinishedProductIssue> FinisedProductIssues { get; set; }
    public DbSet<GoodsReceipt> GoodsReceipts { get; set; }
    public DbSet<FinishedProductReceipt> FinishedProductReceipts { get; set; }
    public DbSet<InventoryLogEntry> InventoryLogEntries { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemClass> ItemClasses { get; set; }
    public DbSet<ItemLot> ItemLots { get; set; }
    public DbSet<FinishedProductInventory> FinishedProductInventories { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<ItemLotLocation> ItemLotLocations { get; set; }

    private IDbContextTransaction? _currentTransaction;
    private readonly IMediator _mediator;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public WarehouseDbContext(DbContextOptions options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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
        modelBuilder.ApplyConfiguration(new FinisedProductIssueEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GoodsReceiptEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new FinishedProductReceiptEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryLogEntryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ItemLotEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new FinishedProductInventoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WarehouseEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ItemClassEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new LocationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ItemLotLocationEntityTypeConfiguration());
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
