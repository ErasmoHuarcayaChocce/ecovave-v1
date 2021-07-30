using ecovave.common;
using ecovave.model;
using ecovave.service.imp;
using ecovave.service.intf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using minedu.tecnologia.util.lib;
using minedu.tecnologia.util.lib.Exceptions;
using OpenTracing;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecovave.backend.Controllers
{
    public class DeliveryController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly ITracer tracer;
        private readonly string environment;

        public DeliveryController(IConfiguration config, ITracer tracer)
        {
            this.config = config;
            this.tracer = tracer;
            this.environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Constante.Produccion;
        }
        [HttpGet("{userId}", Name = "usuario por Id")]
        [Produces("application/json", Type = typeof(StatusResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDeliveryById([FromRoute] int deliveryId)
        {
            if (environment.Equals(Constante.Desarrollo))
            {
                Log.Information("Inicio: Método GetDeliveryById");
                Log.Information("Cadena de conexión: " + config.GetConnectionString("DefaultConnection"));
                Log.Information("IP cliente: " + HttpContext.Connection.RemoteIpAddress.ToString());
            }

            ISpanBuilder builder = tracer.BuildSpan("Get: GetDeliveryById");
            ISpan span = builder.Start();

            try
            {
                span.Log("Inicio: Obtener delivery");
                span.SetTag("Método", "Get:GetDeliveryById");
                IDeliveryService deliveryService = new DeliveryService(config.GetConnectionString("DefaultConnection"));
                DeliveryDto response = await deliveryService.GetDeliveryById(deliveryId);

                span.Finish();

                if (environment.Equals(Constante.Desarrollo)) Log.Information("Fin: Método GetDeliveryById");
                return Ok(new StatusResponse(response));
            }
            catch (NotFoundCustomException ex)
            {
                Log.Error(ex.Message);
                span.Log(ex.Message);
                span.Finish();
                throw ex;
            }
            catch (Exception ex)
            {
                Log.Error(Constante.EX_GENERICA);
                Log.Error(ex.Message);
                span.Log(Constante.EX_GENERICA);
                span.Finish();
                throw ex;
            }
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(StatusResponse))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearDelivery([FromBody] DeliveryDto request)
        {
            if (environment.Equals(Constante.Desarrollo))
            {
                Log.Information("Inicio: Método CrearDelivery");
                Log.Information("Cadena de conexión: " + config.GetConnectionString("DefaultConnection"));
                Log.Information("IP cliente: " + HttpContext.Connection.RemoteIpAddress.ToString());
            }

            ISpanBuilder builder = tracer.BuildSpan("Post::CrearDelivery");
            ISpan span = builder.Start();

            try
            {
                span.Log("Inicio: delivery");
                span.SetTag("Método", "Set:CrearDelivery");
                string ipRegistro = HttpContext.Connection.RemoteIpAddress.ToString();
                request.CreatedIp = ipRegistro;
                int response = 0;
                using (IDeliveryService deliveryService = new DeliveryService(config.GetConnectionString("DefaultConnection")))
                {
                    response = await deliveryService.CrearDelivery(request);
                }
                span.Finish();
                if (environment.Equals(Constante.Desarrollo)) Log.Information("Fin: Método CrearDelivery");
                return Created("", new StatusResponse(response));
            }
            catch (NotFoundCustomException ex)
            {
                Log.Error(ex.Message);
                span.Log(ex.Message);
                span.Finish();
                throw ex;
            }
            catch (Exception ex)
            {
                Log.Error(Constante.EX_GENERICA);
                Log.Error(ex.Message);
                span.Log(Constante.EX_GENERICA);
                span.Finish();
                throw ex;
            }
        }

        [HttpPut()]
        [Produces("application/json", Type = typeof(StatusResponse))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModificarDelivery([FromBody] DeliveryDto request)
        {
            if (environment.Equals(Constante.Desarrollo))
            {
                Log.Information("Inicio: Método ModificarDelivery");
                Log.Information("Cadena de conexión: " + config.GetConnectionString("DefaultConnection"));
                Log.Information("IP cliente: " + HttpContext.Connection.RemoteIpAddress.ToString());
            }

            ISpanBuilder builder = tracer.BuildSpan("Get::ModificarDelivery");
            ISpan span = builder.Start();

            try
            {
                span.Log("Inicio: Modificar la delivery");
                span.SetTag("Método", "Get:ModificarDelivery");
                string ipRegistro = HttpContext.Connection.RemoteIpAddress.ToString();
                request.CreatedIp = ipRegistro;

                int response = 0;
                using (IDeliveryService deliveryService = new DeliveryService(config.GetConnectionString("DefaultConnection")))
                {
                    response = await deliveryService.ModificarDelivery(request);
                }

                span.Finish();

                if (environment.Equals(Constante.Desarrollo)) Log.Information("Fin: Método ModificarDelivery");
                return Created("", new StatusResponse(response));
            }
            catch (NotFoundCustomException ex)
            {
                Log.Error(ex.Message);
                span.Log(ex.Message);
                span.Finish();
                throw ex;
            }
            catch (Exception ex)
            {
                Log.Error(Constante.EX_GENERICA);
                Log.Error(ex.Message);
                span.Log(Constante.EX_GENERICA);
                span.Finish();
                throw ex;
            }
        }
    }
}
