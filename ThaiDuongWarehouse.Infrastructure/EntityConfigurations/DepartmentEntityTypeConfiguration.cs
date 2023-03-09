namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder
            .HasKey(b => b.Id);
        builder
            .Property(b => b.Id);
        builder
            .Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(60);
        builder
            .Ignore(d => d.DomainEvents);
    }
}
