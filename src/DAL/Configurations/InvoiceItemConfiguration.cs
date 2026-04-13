using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    internal class InvoiceItemConfiguration:IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(ii => ii.Id);

            builder.Property(ii => ii.Quantity).IsRequired();

            builder.Property(ii => ii.OriginalPrice).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(ii => ii.DiscountedPrice).HasColumnType("decimal(18,2)");

            builder.Property(ii => ii.ReturnedQuantity).IsRequired().HasDefaultValue(0);

            builder.HasOne(ii => ii.Invoice)
                .WithMany(i => i.InvoiceItems)
                .HasForeignKey(ii => ii.InvoiceId);


            builder.HasOne(ii => ii.BatchItem)
                .WithMany()
                .HasForeignKey(ii => ii.BatchItemId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
