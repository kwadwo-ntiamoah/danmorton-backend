using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jemma.Infrastructure.Persistence.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.BasketNumber);
            builder.HasMany(x => x.BasketItems)
                .WithOne()
                .HasForeignKey(x => x.BasketId);
        }
    }
}