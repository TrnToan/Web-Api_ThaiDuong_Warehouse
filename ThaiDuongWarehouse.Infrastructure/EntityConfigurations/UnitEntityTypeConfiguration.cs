using Unit = ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Unit;

namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class UnitEntityTypeConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.HasKey(u => u.UnitName);
        builder.Property(u => u.UnitName).IsRequired();
    }
}
