using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jemma.Infrastructure.Persistence.Configurations
{
    public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name);

            builder.OwnsOne(x => x.ServiceAmount, priceBuilder => {
                priceBuilder.Property(x => x.Amount)
                    .HasColumnType("decimal(10, 2)");
            });

            
        }
    }
}