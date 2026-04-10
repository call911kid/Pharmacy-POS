using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.BatchItem;

namespace BLL.DTOs.Batch
{
    public class CreateBatchDto
    {
        public int SupplierId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public List<CreateBatchItemDto> BatchItems { get; set; }

        public CreateBatchDto()
        {
            BatchItems = new List<CreateBatchItemDto>();
        }
    }
}
