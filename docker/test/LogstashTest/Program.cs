using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Filters;
using System.IO;

namespace LogstashTest
{
    public class Program
    {
       public static void Main(string[] args)
       {
          BuildWebHost(args).Run();
       }

       public static IWebHost BuildWebHost(string[] args)
       {
          var hostConfig = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("hosting.json", optional: true)
             .AddCommandLine(args)
             .Build();

          return WebHost.CreateDefaultBuilder(args)
             .UseUrls("http://*:5045")
             .UseConfiguration(hostConfig)
             .UseKestrel()
             .UseContentRoot(Directory.GetCurrentDirectory())
             .UseStartup<Startup>()
             //@TODO: push application version as property into global context eg: ver:1.0.0.
             .ConfigureLogging(logging => logging.ClearProviders())
             .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                .Enrich.WithExceptionDetails()
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore"))
                //.Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore"))
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {Properties:j}] {Message:lj}{NewLine}{Exception}{NewLine}")
             )
             .Build();
       }
   }
}
