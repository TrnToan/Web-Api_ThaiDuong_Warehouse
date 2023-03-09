namespace ThaiDuongWarehouse.Infrastructure.EntityConfigurations;
public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .HasKey(e => e.Id);
        builder
            .Property(e => e.Id);
        builder
            .HasIndex(e => e.EmployeeId)
            .IsUnique();
        builder
            .Property(e => e.EmployeeId)
            .HasMaxLength(50);
        builder
            .Property(e => e.EmployeeName)
            .HasMaxLength(50);
        builder
            .Ignore(d => d.DomainEvents);
    }
}
