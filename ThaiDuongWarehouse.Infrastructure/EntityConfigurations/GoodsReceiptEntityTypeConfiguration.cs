namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class GoodsReceiptEntityTypeConfiguration : IEntityTypeConfiguration<GoodsReceipt>
{
    public void Configure(EntityTypeBuilder<GoodsReceipt> builder)
    {
        builder.HasKey(gr => gr.Id);
        builder.HasIndex(gr => gr.GoodsReceiptId).IsUnique();
        builder.Property(gr => gr.Supplier);
        builder.Property(gr => gr.Timestamp).IsRequired();
        builder.Property(gr => gr.IsConfirmed).IsRequired();
        builder.HasOne(gr => gr.Employee).WithMany();

        builder.OwnsMany(gr => gr.Lots, grl =>
        {
            grl.WithOwner().HasForeignKey(l => l.GoodsReceiptId);

            grl.HasKey(lot => lot.GoodsReceiptLotId);
            grl.Property(lot => lot.LocationId);
            grl.Property(lot => lot.Quantity).IsRequired();
            grl.Property(lot => lot.SublotSize);
            grl.Property(lot => lot.PurchaseOrderNumber);
            grl.Property(lot => lot.ProductionDate);
            grl.Property(lot => lot.ExpirationDate);

            grl.HasOne(lot => lot.Item).WithOne().HasForeignKey<GoodsReceiptLot>(lot => lot.ItemId);
            grl.HasIndex(lot => lot.ItemId).IsUnique(false);
            grl.HasOne(e => e.Employee).WithMany().OnDelete(DeleteBehavior.Restrict).IsRequired();
        });
        builder.Ignore(d => d.DomainEvents);
    }
}
