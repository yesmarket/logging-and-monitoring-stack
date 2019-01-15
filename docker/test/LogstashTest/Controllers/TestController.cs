using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prometheus;
using Serilog;
using Serilog.Context;

namespace LogstashTest.Controllers
{
   [Produces("application/json")]
   [Route("api/Test")]
   public class TestController : ControllerBase
   {
      public const string FlatFormat = "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Properties}{NewLine}";

      private static readonly string Host = Environment.MachineName;
      private static readonly Counter Counter = Metrics.CreateCounter("Logs", "Count", "Host", "Type", "Direct");

      private readonly ILogger<TestController> _logger;

      public TestController(ILogger<TestController> logger)
      {
         _logger = logger;
      }

      [HttpGet, MapToApiVersion("1.0")]
      [Consumes("application/json")]
      [Route("d")]
      [ApiExplorerSettings(GroupName = "Test")]
      public ActionResult<string> Default()
      {
         return "working";
      }


      [HttpGet, MapToApiVersion("1.0")]
      [Consumes("application/json")]
      [Route("l")]
      [ApiExplorerSettings(GroupName = "Test")]
      public ActionResult LogstashViaDockerProvider()
      {
         _logger.LogInformation("Logging to Logstash via docker provider");
         Counter.Labels(Host, "Logstash", false.ToString()).Inc();
         return Ok();
      }

      private static void Log(Func<LoggerConfiguration, LoggerConfiguration> func, string message)
      {
         var loggerCoonfiguration = new LoggerConfiguration().Enrich.FromLogContext();
         using (var log = func.Invoke(loggerCoonfiguration).CreateLogger())
         {
            using (LogContext.PushProperty("conversationId", Guid.NewGuid()))
            {
               log.Information(message);
            }
         }
      }
   }
}
