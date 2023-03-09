
using System.Runtime.InteropServices;

namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class GoodsReceiptEntityTypeConfiguration : IEntityTypeConfiguration<GoodsReceipt>
{
    public void Configure(EntityTypeBuilder<GoodsReceipt> builder)
    {
        builder.HasKey(gr => gr.Id);
        builder.HasIndex(gr => gr.GoodsReceiptId).IsUnique();
        builder.Property(gr => gr.PurchaseOrderNumber);
        builder.Property(gr => gr.Timestamp).IsRequired();
        builder.Property(gr => gr.IsConfirmed).IsRequired();

        builder.OwnsMany(gr => gr.Lots, grl =>
        {
            grl.WithOwner().HasForeignKey(lot => lot.GoodsReceiptId);

            grl.HasKey(lot => lot.GoodsReceiptLotId);
            grl.Property(lot => lot.Quantity).IsRequired();
            grl.Property(lot => lot.SublotSize);
            grl.Property(lot => lot.PurchaseOrderNumber);
            grl.Property(lot => lot.ProductionDate);
            grl.Property(lot => lot.ExpirationDate);

            grl.HasOne(lot => lot.Item).WithOne().HasForeignKey<GoodsReceiptLot>(lot => lot.ItemId);
            grl.HasOne(e => e.Employee).WithMany().HasForeignKey(lot => lot.EmployeeId);
        });
        builder.Ignore(d => d.DomainEvents);
    }
}
