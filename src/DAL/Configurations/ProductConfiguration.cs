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
    internal class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Barcode).HasMaxLength(50).IsRequired();

            builder.HasIndex(p => p.Barcode).IsUnique();

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();

           
        }
    }
}
