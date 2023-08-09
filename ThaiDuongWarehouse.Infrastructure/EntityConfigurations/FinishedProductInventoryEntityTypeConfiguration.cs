namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class FinishedProductInventoryEntityTypeConfiguration : IEntityTypeConfiguration<FinishedProductInventory>
{
    public void Configure(EntityTypeBuilder<FinishedProductInventory> builder)
    {
        builder.HasKey(fpi => fpi.Id);
        builder.HasIndex(fpi => new { fpi.PurchaseOrderNumber, fpi.ItemId }).IsUnique();
        builder.Property(fpi => fpi.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(fpi => fpi.PurchaseOrderNumber).IsRequired();
        builder.Property(fpi => fpi.Quantity).IsRequired();

        builder.HasOne(fpi => fpi.Item).WithMany().HasForeignKey(i => i.ItemId).IsRequired();
    }
}
