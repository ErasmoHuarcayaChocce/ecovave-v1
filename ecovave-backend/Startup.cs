using ecovave.backend.Health;
using ecovave.backend.Rabbit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using minedu.rrhh.negocio.comunes.rabbitmq.lib.Historial;
using minedu.rrhh.negocio.comunes.rabbitmq.lib.ReplicaRegistro;
using minedu.tecnologia.tracing.jaeger.lib;
using minedu.tecnologia.util.documentos.rabbitmq.lib;
using minedu.tecnologia.web.lib.Middleware;
using System;

namespace minedu.rrhh.personal.personas.backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", // CorsPolicy
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ecovave-backend",
                    Description = "Datos del maestros de negocio comunes",
                    TermsOfService = new Uri("https://minedu.gob.pe/sisda"),
                    Contact = new OpenApiContact
                    {
                        Name = "SISDA Minedu (DITEN)",
                        Email = string.Empty,
                        Url = new Uri("https://minedu.gob.pe/sisda/contact"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://minedu.gob.pe/sisda/license"),
                    }
                });
            });

            //Configuración de RabbitMQ para publicar y consumir mensajes            
            services.AddRabbitCargaDocumentos(Configuration);
            services.AddRabbitReplicaRegistro(Configuration);
            services.AddRabbitHistorial(Configuration);

            //Configuración de RabbitMQ para consumir mensajes
            services.AddHostedService<RabbitConsumerCentroTrabajoHostedService>();

            //services.Configure<MvcOptions>(options => { options.Filters.Add(new RequireHttpsAttribute()); });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            // Add functionality to inject IOptions<T>
            services.AddOptions();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Para el uso de Jaeger:
            services.AddJaegerTracing(Configuration, tecnologia.tracing.jaeger.lib.Constante.JaegerEntorno.IIS);

            services.AddHealthChecks()
                .AddCheck<ApiHealthCheck>("Api ecovave-backend")
                .AddCheck("db_ayni_personal_licencias Health Check", new SqlServerHealthCheck(Configuration.GetConnectionString("DefaultConnection")), HealthStatus.Unhealthy, new string[] { "db_ayni_personal_licencias" })
                .AddCheck<VersionHealthCheck>("Version Health Check");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionMiddleware();
            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("AllowAll"); // CorsPolicy

            app.UseStaticFiles();
            app.UseSwagger();

            // X-Xss-Protection
            app.UseMiddleware<SecurityHeadersMiddleware>();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ecovave-backend"); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapCustomHealthChecks("ecovave-backend service");
            });
        }
    }
}
