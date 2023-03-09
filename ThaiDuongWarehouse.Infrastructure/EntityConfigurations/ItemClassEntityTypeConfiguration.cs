namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class ItemClassEntityTypeConfiguration : IEntityTypeConfiguration<ItemClass>
{
    public void Configure(EntityTypeBuilder<ItemClass> builder)
    {
        builder.HasKey(i => i.ItemClassId);
        builder.Property(i => i.ItemClassId).IsRequired();
    }
}
