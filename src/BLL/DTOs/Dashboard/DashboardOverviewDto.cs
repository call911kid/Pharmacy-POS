using BLL.DTOs.Batch;

namespace BLL.DTOs.Dashboard
{
    public class DashboardOverviewDto
    {
        public int CustomersCount { get; set; }
        public int SuppliersCount { get; set; }
        public int ProductsCount { get; set; }
        public int BatchesCount { get; set; }
        public List<BatchSummaryDto> RecentBatches { get; set; }
        public List<DashboardInvoiceDto> RecentInvoices { get; set; }

        public DashboardOverviewDto()
        {
            RecentBatches = new List<BatchSummaryDto>();
            RecentInvoices = new List<DashboardInvoiceDto>();
        }
    }
}
