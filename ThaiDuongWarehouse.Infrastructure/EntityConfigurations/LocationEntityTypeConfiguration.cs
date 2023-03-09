namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(l => l.Id);
        builder.HasIndex(l => l.LocationId).IsUnique();
        builder.Property(l => l.LocationId).IsRequired();

        builder.HasMany(i => i.ItemLots).WithOne(l => l.Location).HasForeignKey(l => l.LocationId);
    }   
}
