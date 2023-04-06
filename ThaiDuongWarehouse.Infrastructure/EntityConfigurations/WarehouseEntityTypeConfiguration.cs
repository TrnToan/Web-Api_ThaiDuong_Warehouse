namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class WarehouseEntityTypeConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.HasKey(w => w.Id);
        builder.HasIndex(w => w.WarehouseId).IsUnique();
        builder.Property(w => w.WarehouseName).IsRequired();
        builder.Ignore(d => d.DomainEvents);

        builder.HasMany(l => l.Locations).WithOne().HasForeignKey(l => l.WarehouseId);
    }
}
