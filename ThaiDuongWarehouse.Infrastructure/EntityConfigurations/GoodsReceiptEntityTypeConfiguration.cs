namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class GoodsReceiptEntityTypeConfiguration : IEntityTypeConfiguration<GoodsReceipt>
{
    public void Configure(EntityTypeBuilder<GoodsReceipt> builder)
    {
        builder.HasKey(gr => gr.Id);
        builder.HasIndex(gr => gr.GoodsReceiptId).IsUnique();
        builder.Property(gr => gr.Supplier);
        builder.Property(gr => gr.Timestamp).IsRequired();
        builder.HasOne(gr => gr.Employee).WithMany();

        builder.OwnsMany(gr => gr.Lots, grl =>
        {
            grl.WithOwner().HasForeignKey(l => l.GoodsReceiptId);

            grl.HasKey(lot => lot.Id);
            grl.HasIndex(lot => lot.GoodsReceiptLotId).IsUnique();
            grl.Property(lot => lot.LocationId);
            grl.Property(lot => lot.Quantity).IsRequired();           
            grl.Property(lot => lot.ProductionDate);
            grl.Property(lot => lot.ExpirationDate);

            grl.HasOne(lot => lot.Item).WithMany().HasForeignKey(lot => lot.ItemId);
            grl.HasOne(e => e.Employee).WithMany().OnDelete(DeleteBehavior.Restrict).IsRequired();
        });
        builder.Ignore(d => d.DomainEvents);
    }
}
