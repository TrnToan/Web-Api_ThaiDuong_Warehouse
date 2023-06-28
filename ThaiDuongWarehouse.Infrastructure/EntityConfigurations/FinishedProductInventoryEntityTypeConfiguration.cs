namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class FinishedProductInventoryEntityTypeConfiguration : IEntityTypeConfiguration<FinishedProductInventory>
{
    public void Configure(EntityTypeBuilder<FinishedProductInventory> builder)
    {
        builder.HasKey(fpi => fpi.Id);
        builder.Property(fpi => fpi.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(fpi => fpi.PurchaseOrderNumber).IsRequired();
        builder.Property(fpi => fpi.Quantity).IsRequired();
        builder.Property(fpi => fpi.Timestamp).IsRequired();

        builder.HasOne(fpi => fpi.Item).WithMany().IsRequired();
    }
}
