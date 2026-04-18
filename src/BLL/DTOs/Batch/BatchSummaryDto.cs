using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Batch
{
    public class BatchSummaryDto
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int ItemsCount { get; set; }
        public int TotalQuantityReceived { get; set; }
        public int TotalQuantityRemaining { get; set; }
    }
}
