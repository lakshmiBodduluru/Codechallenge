using System;
using DataExtractEngine;
using DataExtractEngineInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace dotnet_code_challenge
{
    public class Program
    {
        //public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddTransient<IHorseDataExtractor,HorseDataXMLExtractor>()
                .BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>();
            //.AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //do the actual work here
            //var bar = serviceProvider.GetService<IBarService>();
            //bar.DoSomeRealWork();

            logger.LogDebug("All done!");
        }
    }
}
