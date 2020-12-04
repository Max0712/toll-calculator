using System;
using Toll_DbMigrator.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Toll_DbMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();
            var isUp = true;
            Int64 targetVersion = 0;

            Directory.CreateDirectory("c:\\temp");

            using (var scope = serviceProvider.CreateScope())
            {
                Console.WriteLine("Write migrate UP or DOWN");
                var input = Console.ReadLine();

                if (input.ToCharArray()[0] == 'D' || input.ToCharArray()[0] == 'd')
                {
                    isUp = false;

                    Console.WriteLine("Target downgrade migration ID");
                    targetVersion = int.Parse(Console.ReadLine());
                }

                UpdateDatabase(scope.ServiceProvider, targetVersion, isUp);
            }
        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()                
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()                    
                    .WithGlobalConnectionString("Data Source=c:\\temp\\toll.db")                    
                    .ScanIn(typeof(_0001_AddTable_Log).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider, Int64 targetVersion, bool up = true)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            if (up)
                runner.MigrateUp();

            if (!up)            
                runner.MigrateDown(targetVersion);            
        }
    }
}
