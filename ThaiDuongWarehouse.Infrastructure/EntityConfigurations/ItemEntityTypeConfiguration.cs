namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class ItemEntityTypeConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.Id);
        builder.HasIndex(i => new { i.ItemId, i.Unit }).IsUnique();
        builder.Property(i => i.ItemName).IsRequired();
        builder.Property(i => i.Unit).IsRequired();
        builder.Property(i => i.MinimumStockLevel).IsRequired();
        builder.Property(i => i.Price).HasPrecision(12, 2);
        builder.Property(i => i.SublotSize);
        builder.Property(i => i.PacketUnit);
        builder.Ignore(d => d.DomainEvents);

        builder.HasOne(i => i.ItemClass).WithMany(i => i.Items).HasForeignKey(i => i.ItemClassId);
    }
}
