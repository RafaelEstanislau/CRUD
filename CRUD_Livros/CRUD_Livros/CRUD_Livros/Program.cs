using CRUD_Livros.Infra.AcessoDeDados;
using CRUD_Livros.UserInterface;
using Infra.AcessoDeDados;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CRUD_Livros.Migrations;
namespace CRUD_Livros
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BancoDeDados.RunMigrations();
            var builder = CreateHostBuilder();
            var servicesProvider = builder.Build().Services;
            var repositorio = servicesProvider.GetService<IRepositorio>();



            ApplicationConfiguration.Initialize();
            Application.Run(new FormularioExibicao(repositorio));
        }



        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddScoped<IRepositorio, RepositoryLINQTODB>();
                    
                });
        }
    }
}