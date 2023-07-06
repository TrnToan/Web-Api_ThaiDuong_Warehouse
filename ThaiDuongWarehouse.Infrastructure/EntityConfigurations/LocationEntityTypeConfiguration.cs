namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(l => l.Id);
        builder.HasIndex(l => l.LocationId).IsUnique();
        builder.Property(l => l.LocationId).IsRequired();
        builder.Ignore(d => d.DomainEvents);

        builder.HasMany(i => i.ItemLots).WithMany(l => l.Locations);
    }   
}
