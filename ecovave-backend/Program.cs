using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Elastic.CommonSchema.Serilog;
using Microsoft.AspNetCore.Http;

namespace minedu.rrhh.personal.personas.backend
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            // var currentDir = Directory.GetCurrentDirectory();
            string jsonFile;
            if (environment.Equals(Environments.Development))
                jsonFile = $"appsettings.{environment}.json";
            else
                jsonFile = "appsettings.json";

            var Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(jsonFile, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            //inicio: modificado para el uso de serilog
           string serviceName = Assembly.GetEntryAssembly().GetName().Name;
            string fileLogName = serviceName + ".log"; //comentar esta línea en producción
            string pathFileLog = "/logs/apps/ecovave-backend/" + fileLogName; //comentar esta línea en producción
            LogEventLevel logLevel;
            if (environment.Equals("Production"))
                logLevel = LogEventLevel.Information;
            else
                logLevel = LogEventLevel.Verbose;

            //Configuración del logging
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                //.MinimumLevel.Debug() //comentar esta línea en producción
                .MinimumLevel.Information() //descomentar esta línea en producción
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning) //descomentar esta línea en producción
                .Enrich.WithProperty("Service", serviceName)
                .Enrich.WithProperty("ENV", environment)
                .WriteTo.Console(new EcsTextFormatter(new EcsTextFormatterConfiguration().MapHttpContext(Configuration.Get<HttpContextAccessor>())), logLevel)
                .WriteTo.File(new EcsTextFormatter(new EcsTextFormatterConfiguration().MapHttpContext(Configuration.Get<HttpContextAccessor>())), pathFileLog, logLevel) //comentar esta línea en producción para kubernetes
                .CreateLogger();

            try
            {
                Log.Information("Iniciando el servicio " + serviceName);
                Log.Information("ASPNETCORE_ENVIRONMENT: " + environment);

                CreateHostBuilder(args).Build().Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "El Host terminó inesperadamente");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
            //fin: modificado para el uso de serilog
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog(); //para el uso de logging serilog
    }
}
