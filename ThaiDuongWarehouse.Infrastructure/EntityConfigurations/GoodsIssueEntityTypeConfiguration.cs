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
            .Property(g => g.Timestamp)
            .IsRequired();
        builder
            .HasOne(g => g.Employee)
            .WithMany()
            .HasForeignKey(g => g.EmployeeId);
        
        builder.OwnsMany(g => g.Entries, ge =>
        {
            ge.WithOwner().HasForeignKey(ge => ge.GoodsIssueId);

            ge.HasKey(entry => entry.Id);
            ge.Property(entry => entry.Id).ValueGeneratedOnAdd().IsRequired();
            ge.Property(entry => entry.RequestedQuantity).IsRequired();

            ge.HasOne(entry => entry.Item).WithMany().IsRequired().HasForeignKey(entry => entry.ItemId);
            ge.OwnsMany(entry => entry.Lots, lot =>
            {
                lot.WithOwner();
                lot.HasKey(gil => new { gil.GoodsIssueEntryId, gil.GoodsIssueLotId });
                lot.Property(gil => gil.Quantity).IsRequired();
                lot.Property(gil => gil.Note);

                lot.HasOne(gil => gil.Employee).WithMany().HasForeignKey(gil => gil.EmployeeId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            });
        });
        builder.Ignore(d => d.DomainEvents);
    }
}
