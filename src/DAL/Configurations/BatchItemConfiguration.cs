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
    internal class BatchItemConfiguration:IEntityTypeConfiguration<BatchItem>
    {
        public void Configure(EntityTypeBuilder<BatchItem> builder)
        {
            builder.HasKey(bi => bi.Id);

            builder.Property(bi => bi.QuantityReceived).IsRequired();

            builder.Property(bi => bi.QuantityRemaining).IsRequired();

            builder.Property(bi => bi.ExpirationDate).IsRequired();

            builder.Property(bi => bi.CostPrice).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(bi => bi.MandatorySellingPrice).HasColumnType("decimal(18,2)").IsRequired();

            
            builder.HasOne(bi => bi.Batch)
                .WithMany(b => b.BatchItems)
                .HasForeignKey(bi => bi.BatchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bi => bi.Product)
                .WithMany(p => p.BatchItems)
                .HasForeignKey(bi => bi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
