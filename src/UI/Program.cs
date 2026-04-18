using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;
using BLL.DependencyInjection;
using DAL.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UI.Exceptions;
using UI.Events;
using UI.Forms;
using UI.Forms.BatchDialog;
using UI.Forms.Main;
using UI.Forms.SupplierDialog;
using UI.Views.Customers;
using UI.Views.Inventory;
using UI.Views.POS;
using UI.Views.Suppliers;

namespace UI
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            Application.ThreadException += (sender, e) =>
                ExceptionHandler.Handle(e.Exception);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                ExceptionHandler.Handle(e.ExceptionObject as Exception);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            var builder = WebApplication.CreateBuilder(args);


            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(8443, listenOptions =>
                {
                    listenOptions.UseHttps();
                });
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");


            builder.Services
                .AddDALRepositories(connectionString)
                .AddBLLServices()
                .AddSingleton<ScannerEventBus>()
                .AddTransient<ProductEditorForm>()
                .AddTransient<SupplierDialog>()
                .AddTransient<BatchDialog>()
                .AddTransient<ScannerConnectionForm>()
                .AddTransient<CustomersPage>()
                .AddTransient<InventoryPage>()
                .AddTransient<PosPage>()
                .AddTransient<SuppliersPage>()
                .AddTransient<Main>();

            var app = builder.Build();
            app.UseStaticFiles();

            app.MapGet("/", () => Results.File(Path.Combine(builder.Environment.WebRootPath, "scanner.html"), "text/html"));


            app.MapPost("/api/barcode", async (HttpContext context, ScannerEventBus eventBus) =>
            {
                using var reader = new StreamReader(context.Request.Body);
                var body = await reader.ReadToEndAsync();

                var jsonDoc = JsonDocument.Parse(body);
                var barcode = jsonDoc.RootElement.GetProperty("barcode").GetString();


                eventBus.PublishScan(barcode);

                return Results.Ok(new
                {
                    success = true,
                    barcode = barcode,
                    timestamp = DateTime.Now.ToString("HH:mm:ss"),
                    total = 1
                });
            });

            app.MapPost("/api/clear", () => Results.Ok(new { success = true }));


            _ = app.StartAsync();


            using var scope = app.Services.CreateScope();
            var mainForm = scope.ServiceProvider.GetRequiredService<Main>();
            Application.Run(mainForm);


            app.StopAsync().Wait();
        }
    }
}
