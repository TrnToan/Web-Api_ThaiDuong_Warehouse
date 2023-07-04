namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class FinishedProductReceiptEntityTypeConfiguration : IEntityTypeConfiguration<FinishedProductReceipt>
{
    public void Configure(EntityTypeBuilder<FinishedProductReceipt> builder)
    {
        builder.HasKey(fpr => fpr.Id);
        builder.HasIndex(fpr => fpr.FinishedProductReceiptId).IsUnique();
        builder.Property(fpr => fpr.Timestamp);

        builder.HasOne(fpr => fpr.Employee).WithMany().HasForeignKey(fpr => fpr.EmployeeId).IsRequired();
        builder.OwnsMany(fpr => fpr.Entries, fpre =>
        {
            fpre.WithOwner().HasForeignKey(fpre => fpre.FinishedProductReceiptId);

            fpre.HasKey(fpre => fpre.Id);
            fpre.Property(fpre => fpre.Id).ValueGeneratedOnAdd().IsRequired();
            fpre.Property(fpre => fpre.PurchaseOrderNumber).IsRequired();
            fpre.Property(fpre => fpre.Quantity).IsRequired();
            fpre.Property(fpre => fpre.Note);

            fpre.HasOne(fpre => fpre.Item).WithMany().IsRequired();
        });
        builder.Ignore(fpr => fpr.DomainEvents);
    }
}
