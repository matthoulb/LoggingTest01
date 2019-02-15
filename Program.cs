using System;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.ApplicationInsights;
using Sentry;

namespace LogTest
{
    class Program
    {
        static void Main(string[] args)
        {
	        Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Sentry(x =>
                {
                    x.Environment = "Local";
                    x.Release = "0.1";
                    x.MinimumBreadcrumbLevel = LogEventLevel.Debug;
                    x.MinimumEventLevel = LogEventLevel.Debug;
                    x.Dsn = new Sentry.Dsn("https://68d8e7cd08ff43b4a2da4a70b3b287d9@sentry.io/1394297");
                })
                .WriteTo.ApplicationInsightsEvents("1b5750c0-6d46-4dc8-888f-3083403acf5e")
                .CreateLogger();

            SentrySdk.ConfigureScope(scope =>
            {
                scope.SetTag("my-tag", "my value");
            });

            var rand = new Random();
            var arr = new int[1];
            //for (var a = 0;a < 5;a++) {
                for (var i = 0;i < rand.Next(0, 50);i++) {
                    try
                    {
                        var b = arr[i];
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, ex.Message);
                    }
                }
                // for (var i = 0;i < rand.Next(0, 50);i++) {
                //     Log.Fatal("Downloading {Version}", i);
                // }
                // for (var i = 0;i < rand.Next(0, 50);i++) {
                //     Log.Warning("Downloading {Version}", i);
                // }
            //}

            Console.ReadLine();
        }
    }
}
