namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class LotAdjustmentEntityTypeConfiguration : IEntityTypeConfiguration<LotAdjustment>
{
    public void Configure(EntityTypeBuilder<LotAdjustment> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.LotId).IsRequired();
        builder.Property(l => l.BeforeQuantity).IsRequired();
        builder.Property(l => l.AfterQuantity).IsRequired();
        builder.Property(l => l.IsConfirmed).IsRequired();
        builder.Property(l => l.Timestamp).IsRequired();
        builder.Property(l => l.Note);
        builder.Ignore(d => d.DomainEvents);

        builder.HasOne(e => e.Employee).WithMany().HasForeignKey(la => la.EmployeeId).IsRequired();
        builder.HasOne(i => i.Item).WithMany().HasForeignKey(la => la.ItemId).IsRequired();
        builder.OwnsMany(la => la.SublotAdjustments, sub =>
        {
            sub.WithOwner();

            sub.Property(lot => lot.LocationId).IsRequired();
            sub.Property(lot => lot.BeforeQuantityPerLocation);
            sub.Property(lot => lot.AfterQuantityPerLocation);
        });
    }
}