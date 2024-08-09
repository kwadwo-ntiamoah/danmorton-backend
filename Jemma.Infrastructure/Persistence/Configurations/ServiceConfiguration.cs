using Jemma.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jemma.Infrastructure.Persistence.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name);
            builder.OwnsOne(x => x.Price, priceBuilder => {
                priceBuilder.Property(x => x.Amount)
                    .HasColumnType("decimal(10, 2)");
            });
        }
    }
}