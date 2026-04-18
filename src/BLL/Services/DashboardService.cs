using BLL.DTOs.Dashboard;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    internal class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBatchService _batchService;

        public DashboardService(IUnitOfWork unitOfWork, IBatchService batchService)
        {
            _unitOfWork = unitOfWork;
            _batchService = batchService;
        }

        public async Task<DashboardOverviewDto> GetOverviewAsync(
            int recentInvoicesCount = 5,
            int recentBatchesCount = 5,
            int lowStockThreshold = 10,
            int alertsCount = 5)
        {
            recentInvoicesCount = Math.Max(1, recentInvoicesCount);
            recentBatchesCount = Math.Max(1, recentBatchesCount);
            lowStockThreshold = Math.Max(1, lowStockThreshold);
            alertsCount = Math.Max(1, alertsCount);

            var customers = (await _unitOfWork.Customers.GetAllAsync(1, int.MaxValue)).ToList();
            var suppliers = (await _unitOfWork.Suppliers.GetAllAsync(1, int.MaxValue)).ToList();
            var products = (await _unitOfWork.Products.GetAllAsync(1, int.MaxValue)).ToList();
            var invoices = (await _unitOfWork.Invoices.GetAllAsync(1, int.MaxValue)).ToList();
            var batches = (await _batchService.GetBatchSummariesAsync(1, int.MaxValue)).ToList();
            var lowStockItems = (await _unitOfWork.BatchItems.GetLowStockItemsAsync(lowStockThreshold, alertsCount)).ToList();
            var expiringItems = (await _unitOfWork.BatchItems.GetExpiringItemsAsync(DateTime.Now.AddDays(30), alertsCount)).ToList();

            return new DashboardOverviewDto
            {
                CustomersCount = customers.Count,
                SuppliersCount = suppliers.Count,
                ProductsCount = products.Count,
                BatchesCount = batches.Count,
                RecentBatches = batches
                    .OrderByDescending(batch => batch.PurchaseDate)
                    .Take(recentBatchesCount)
                    .ToList(),
                RecentInvoices = invoices
                    .OrderByDescending(invoice => invoice.InvoiceDate)
                    .Take(recentInvoicesCount)
                    .Select(invoice => new DashboardInvoiceDto
                    {
                        Id = invoice.Id,
                        CustomerId = invoice.CustomerId,
                        InvoiceDate = invoice.InvoiceDate,
                        TotalAmount = invoice.TotalAmount,
                        Status = invoice.Status.ToString()
                    })
                    .ToList(),
                LowStockItems = lowStockItems
                    .Select(item => new DashboardAlertItemDto
                    {
                        BatchItemId = item.Id,
                        ProductId = item.ProductId,
                        ProductName = item.Product?.Name ?? $"Product #{item.ProductId}",
                        QuantityRemaining = item.QuantityRemaining,
                        ExpirationDate = item.ExpirationDate
                    })
                    .ToList(),
                ExpiringItems = expiringItems
                    .Select(item => new DashboardAlertItemDto
                    {
                        BatchItemId = item.Id,
                        ProductId = item.ProductId,
                        ProductName = item.Product?.Name ?? $"Product #{item.ProductId}",
                        QuantityRemaining = item.QuantityRemaining,
                        ExpirationDate = item.ExpirationDate
                    })
                    .ToList()
            };
        }
    }
}
