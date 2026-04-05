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
            
        }
    }
}
