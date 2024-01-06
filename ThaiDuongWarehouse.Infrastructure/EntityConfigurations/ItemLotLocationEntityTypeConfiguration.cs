using ThaiDuongWarehouse.Domain.AggregateModels.ItemLotLocationAggregate;

namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class ItemLotLocationEntityTypeConfiguration : IEntityTypeConfiguration<ItemLotLocation>
{
    public void Configure(EntityTypeBuilder<ItemLotLocation> builder)
    {
        builder.HasKey(il => new { il.ItemLotId, il.LocationId });
        builder.Property(il => il.QuantityPerLocation);

        builder.HasOne(ill => ill.ItemLot).WithMany(il => il.ItemLotLocations).HasForeignKey(il => il.ItemLotId);
        builder.HasOne(ill => ill.Location).WithMany(l => l.ItemLotLocations).HasForeignKey(il => il.LocationId);
    }
}
