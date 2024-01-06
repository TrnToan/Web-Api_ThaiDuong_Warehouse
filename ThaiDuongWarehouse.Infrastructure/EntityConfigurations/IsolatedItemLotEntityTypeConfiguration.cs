using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
internal class IsolatedItemLotEntityTypeConfiguration : IEntityTypeConfiguration<IsolatedItemLot>
{
    public void Configure(EntityTypeBuilder<IsolatedItemLot> builder)
    {
        builder.HasKey(it => it.ItemLotId);
        builder.Property(it => it.Quantity).IsRequired();
        builder.Property(it => it.ProductionDate);
        builder.Property(it => it.ExpirationDate);
        builder.Ignore(it => it.DomainEvents);

        builder.HasOne(it => it.Item).WithMany().HasForeignKey(it => it.ItemId);
    }
}
