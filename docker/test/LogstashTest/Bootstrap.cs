using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using Serilog;
using Microsoft.AspNetCore.Mvc;

namespace LogstashTest
{
   public static class Bootstrap
   {
      public static void ConfigureServices(IServiceCollection serviceCollection, IConfiguration config)
      {
         serviceCollection.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
         serviceCollection.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      }
   }
}
