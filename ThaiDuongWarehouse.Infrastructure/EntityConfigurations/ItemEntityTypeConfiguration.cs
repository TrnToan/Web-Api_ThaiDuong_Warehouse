namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class ItemEntityTypeConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.Id);
        builder.HasIndex(i => i.ItemId).IsUnique();
        builder.Property(i => i.ItemName).IsRequired();
        builder.Property(i => i.MinimumStockLevel).IsRequired();
        builder.Property(i => i.Price).HasPrecision(12, 2).IsRequired();
        builder.Ignore(d => d.DomainEvents);

        builder.HasOne(u => u.Unit).WithMany().HasForeignKey(i => i.UnitName);
        builder.HasOne(i => i.ItemClass).WithMany(i => i.Items).HasForeignKey(i => i.ItemClassId);
    }
}
