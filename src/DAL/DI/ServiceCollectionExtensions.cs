using DAL.Context;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDALRepositories(
        this IServiceCollection services,
        string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddDbContext<PharmacyDbContext>(options =>
            options.UseLazyLoadingProxies()
                   .UseSqlServer(connectionString));

        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBatchRepository, BatchRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IBatchItemRepository, BatchItemRepository>();
        services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
