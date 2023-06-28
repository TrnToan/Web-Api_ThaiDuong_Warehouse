namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class ItemLotEntityTypeConfiguration : IEntityTypeConfiguration<ItemLot>
{
    public void Configure(EntityTypeBuilder<ItemLot> builder)
    {
        builder.HasKey(l => l.Id);
        builder.HasIndex(l => l.LotId).IsUnique();
        builder.Property(l => l.Timestamp).IsRequired();
        builder.Property(l => l.Quantity).IsRequired();
        builder.Property(l => l.ProductionDate);
        builder.Property(l => l.ExpirationDate);
        builder.Property(l => l.IsIsolated).IsRequired();
        builder.Ignore(d => d.DomainEvents);

        builder.HasOne(i => i.Item).WithMany().HasForeignKey(i => i.ItemId);
    }
}
