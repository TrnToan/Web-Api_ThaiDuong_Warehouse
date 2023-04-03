namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class InventoryLogEntryEntityTypeConfiguration : IEntityTypeConfiguration<InventoryLogEntry>
{
    public void Configure(EntityTypeBuilder<InventoryLogEntry> builder)
    {
        builder.HasKey(log => log.Id);
        builder.Ignore(d => d.DomainEvents);
        builder.Property(log => log.ItemLotId).IsRequired();
        builder.Property(log => log.Timestamp).IsRequired();
        builder.Property(log => log.BeforeQuantity).IsRequired();
        builder.Property(log => log.ChangedQuantity).IsRequired();
        builder.Property(log => log.Unit).IsRequired();

        builder.HasOne(log => log.Item).WithMany().HasForeignKey(log => log.ItemId);
    }
}
