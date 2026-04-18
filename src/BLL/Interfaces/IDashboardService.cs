using BLL.DTOs.Dashboard;

namespace BLL.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardOverviewDto> GetOverviewAsync(
            int recentInvoicesCount = 5,
            int recentBatchesCount = 5,
            int lowStockThreshold = 10,
            int alertsCount = 5);
    }
}
