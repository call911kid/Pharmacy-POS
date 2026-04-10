using BLL.DependencyInjection;
using DAL.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UI.Exceptions;

namespace UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Application.ThreadException += (sender, e) =>
                ExceptionHandler.Handle(e.Exception);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                ExceptionHandler.Handle(e.ExceptionObject as Exception);

            var builder = Host.CreateApplicationBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

            builder.Services
                .AddDALRepositories(connectionString)
                .AddBLLServices()
                .AddTransient<MainForm>();

            using var host = builder.Build();
            using var scope = host.Services.CreateScope();

            ApplicationConfiguration.Initialize();
            Application.Run(scope.ServiceProvider.GetRequiredService<MainForm>());
        }
    }
}
