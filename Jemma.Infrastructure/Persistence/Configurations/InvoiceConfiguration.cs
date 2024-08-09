using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jemma.Infrastructure.Persistence.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.InvoiceNumber);

            builder.HasMany(x => x.InvoiceItems)
                .WithOne()
                .HasForeignKey(x => x.InvoiceId);

            builder.OwnsOne(x => x.AmountPaid, priceBuilder => {
                priceBuilder.Property(x => x.Amount)
                    .HasColumnType("decimal(10, 2)");
            });

            builder.OwnsOne(x => x.TotalAmount, priceBuilder => {
                priceBuilder.Property(x => x.Amount)
                    .HasColumnType("decimal(10, 2)");
            });

            builder.OwnsOne(x => x.BillTo);
        }
    }
}