using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.BatchItem;
using DAL.Models;

namespace BLL.DTOs.Batch
{
    public class BatchDto
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SupplierId { get; set; }
        public ICollection<BatchItemDto> Items { get; set; }

        public BatchDto()
        {
            Items = new List<BatchItemDto>();
        }

    }
}
