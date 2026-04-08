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
    internal class InvoiceConfiguration:IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.InvoiceDate).IsRequired();

            builder.Property(i => i.Barcode).HasMaxLength(50).IsRequired();
            
            builder.Property(i => i.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(i => i.TotalDiscount).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(i => i.NetAmount).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
