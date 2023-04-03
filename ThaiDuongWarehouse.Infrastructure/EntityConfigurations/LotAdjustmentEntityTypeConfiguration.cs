namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class LotAdjustmentEntityTypeConfiguration : IEntityTypeConfiguration<LotAdjustment>
{
    public void Configure(EntityTypeBuilder<LotAdjustment> builder)
    {
        builder.HasKey(l => l.Id);
        builder.HasIndex(l => l.LotId).IsUnique();
        builder.Property(l => l.NewPurchaseOrderNumber).HasMaxLength(50).IsRequired();
        builder.Property(l => l.OldPurchaseOrderNumber).HasMaxLength(50).IsRequired();
        builder.Property(l => l.BeforeQuantity).IsRequired();
        builder.Property(l => l.AfterQuantity).IsRequired();
        builder.Property(l => l.Unit).IsRequired();
        builder.Property(l => l.IsConfirmed).IsRequired();
        builder.Property(l => l.Timestamp).IsRequired();
        builder.Property(l => l.Note);
        builder.Ignore(d => d.DomainEvents);

        builder.HasOne(e => e.Employee).WithMany().HasForeignKey(la => la.EmployeeId).IsRequired();
        builder.HasOne(i => i.Item).WithOne().HasForeignKey<LotAdjustment>(la => la.ItemId).IsRequired();
    }
}