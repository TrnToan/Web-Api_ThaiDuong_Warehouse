namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class FinisedProductIssueEntityTypeConfiguration : IEntityTypeConfiguration<FinishedProductIssue>
{
    public void Configure(EntityTypeBuilder<FinishedProductIssue> builder)
    {
        builder.HasKey(fpr => fpr.Id);
        builder.HasIndex(fpr => fpr.FinishedProductIssueId).IsUnique();
        builder.Property(fpr => fpr.Timestamp);

        builder.HasOne(fpr => fpr.Employee).WithMany().HasForeignKey(fpr => fpr.EmployeeId).IsRequired();
        builder.OwnsMany(fpr => fpr.Entries, fpre =>
        {
            fpre.WithOwner().HasForeignKey(fpre => fpre.FinishedProductIssueId);

            fpre.HasKey(fpre => fpre.Id);
            fpre.HasIndex(fpre => new { fpre.ItemId, fpre.PurchaseOrderNumber, fpre.FinishedProductIssueId }).IsUnique();
            fpre.Property(fpre => fpre.Id).ValueGeneratedOnAdd().IsRequired();
            fpre.Property(fpre => fpre.PurchaseOrderNumber).IsRequired();
            fpre.Property(fpre => fpre.Quantity).IsRequired();
            fpre.Property(fpre => fpre.Note);

            fpre.HasOne(fpre => fpre.Item).WithMany().HasForeignKey(e => e.ItemId).IsRequired();
        });
        builder.Ignore(fpr => fpr.DomainEvents);
    }
}
