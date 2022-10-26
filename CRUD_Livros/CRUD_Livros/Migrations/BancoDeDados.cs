using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD_Livros.Migrations
{
    public class BancoDeDados
    {
        public static void RunMigrations()
        {
            //Coloca o banco de dados em escopo
            var serviceProvider = CreateServices();
            using (var scope = serviceProvider.CreateScope())
            {
                RunMigrations(scope.ServiceProvider);
            }
        }
        //Configura os serviços com injeção de dependência
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(ConnectionString)
                    
                    .ScanIn(typeof(BancoDeDados).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
        private static void RunMigrations(IServiceProvider serviceProvider)
        {
            //Instancia o runner e executa a migration
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
            //runner.MigrateDown(051020221125);
        }

        private const string ConnectionString = "Server = INVENT127; Database = Livros; User Id = sa; Password = sap@123";
    }
}
