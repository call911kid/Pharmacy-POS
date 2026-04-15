using BLL.Interfaces;
using BLL.DTOs.Refund;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBLLServices(this IServiceCollection services)
    {
        services.AddScoped<IBatchService, BatchService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IRefundService, RefundService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IProductService, ProductService>();


        return services;
    }
    
}
