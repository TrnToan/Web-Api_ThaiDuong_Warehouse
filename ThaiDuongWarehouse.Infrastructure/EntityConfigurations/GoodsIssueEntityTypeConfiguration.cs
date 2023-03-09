namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class GoodsIssueEntityTypeConfiguration : IEntityTypeConfiguration<GoodsIssue>
{
    public void Configure(EntityTypeBuilder<GoodsIssue> builder)
    {
        builder
            .HasKey(g => g.Id);
        builder
            .HasIndex(g => g.GoodsIssueId)
            .IsUnique();
        builder
            .Property(g => g.Receiver);
        builder
            .Property(g => g.PurchaseOrderNumber);
        builder
            .Property(g => g.IsConfirmed)
            .IsRequired();
        builder
            .Property(g => g.Timestamp)
            .IsRequired();

        builder.OwnsMany(g => g.Entries, ge =>
        {
            ge.WithOwner().HasForeignKey(ge => ge.GoodsIssueId);

            ge.HasKey(entry => entry.Id);
            ge.Property(entry => entry.Id).ValueGeneratedOnAdd().IsRequired();
            ge.Property(entry => entry.RequestedSublotSize);
            ge.Property(entry => entry.RequestedQuantity).IsRequired();

            ge.HasOne(entry => entry.Item).WithOne().IsRequired(); // Kiểm tra lại mối quan hệ giữa Item và GoodsIssueEntry
            ge.OwnsMany(entry => entry.Lots, lot =>
            {
                lot.WithOwner();
                lot.HasKey(p => p.GoodsIssueLotId);
                lot.Property(p => p.Quantity).IsRequired();
                lot.Property(p => p.SublotSize);
                lot.Property(p => p.Note);

                lot.HasOne(e => e.Employee).WithMany().HasForeignKey(e => e.EmployeeId).IsRequired();
            });
        });
        builder.Ignore(d => d.DomainEvents);
    }
}
