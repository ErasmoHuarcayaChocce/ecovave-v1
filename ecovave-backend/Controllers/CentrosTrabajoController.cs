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
using System.Threading.Tasks;

namespace ecovave.backend.Controllers
{
    [Route("v1/ecovave/centrostrabajo")]
    [ApiController]
    public class CentrosTrabajoController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly ITracer tracer;
        private readonly string environment;

        public CentrosTrabajoController(IConfiguration config, ITracer tracer)
        {
            this.config = config;
            this.tracer = tracer;
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Constante.Produccion;
        }

        [HttpGet("buscar", Name = "GetCentroTrabajo")]
        [Produces("application/json", Type = typeof(StatusResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCentroTrabajo([FromQuery] CentroTrabajoConsulta request)
        {
            if (environment.Equals(Constante.Desarrollo))
            {
                Log.Information("Inicio: Método GetCentroTrabajo");
                Log.Information("Cadena de conexión: " + config.GetConnectionString("DefaultConnection"));
                Log.Information("IP cliente: " + HttpContext.Connection.RemoteIpAddress.ToString());
            }

            ISpanBuilder builder = tracer.BuildSpan("Get::GetCentroTrabajo");
            ISpan span = builder.Start();

            try
            {
                span.Log("Inicio: Obtiene el centro de trabajo por código de centro de trabajo");
                span.SetTag("Método", "Get:GetCentroTrabajo");

                ICentroTrabajoService centroTrabajoService = new CentroTrabajoService(config.GetConnectionString("DefaultConnection"));
                CentroTrabajoRegistro response = await centroTrabajoService.GetCentroTrabajo(request);

                span.Finish();

                if (environment.Equals(Constante.Desarrollo)) Log.Information("Fin: Método GetCentroTrabajo");
                return Ok(new StatusResponse(response));
            }
            catch (ValidationCustomException ex)
            {
                Log.Error(ex.Message);
                span.Log(ex.Message);
                span.Finish();
                throw ex;
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

        [HttpGet]
        [Produces("application/json", Type = typeof(StatusResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListaCentroTrabajo([FromQuery] CentroTrabajoFiltro request)
        {
            if (environment.Equals(Constante.Desarrollo))
            {
                Log.Information("Inicio: Método ListaCentroTrabajo");
                Log.Information("Cadena de conexión: " + config.GetConnectionString("DefaultConnection"));
                Log.Information("IP cliente: " + HttpContext.Connection.RemoteIpAddress.ToString());
            }

            ISpanBuilder builder = tracer.BuildSpan("Get::ListaCentroTrabajo");
            ISpan span = builder.Start();

            try
            {
                span.Log("Inicio: Lista los centros de trabajo por búsqueda personalizada");
                span.SetTag("Método", "Get:ListaCentroTrabajo");

                ICentroTrabajoService centroTrabajoService = new CentroTrabajoService(config.GetConnectionString("DefaultConnection"));
                IEnumerable<CentroTrabajoGrilla> response = await centroTrabajoService.ListaCentroTrabajo(request);

                span.Finish();

                if (environment.Equals(Constante.Desarrollo)) Log.Information("Fin: Método ListaCentroTrabajo");
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

      

        [HttpGet("buscarporcodigo", Name = "Buscar centro de trabajo")]
        [Produces("application/json", Type = typeof(StatusResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarCentroTrabajo([FromQuery] string codigoCentroTrabajo, bool? activo)
        {
            if (environment.Equals(Constante.Desarrollo))
            {
                Log.Information("Inicio: Método BuscarCentroTrabajo");
                Log.Information("Cadena de conexión: " + config.GetConnectionString("DefaultConnection"));
                Log.Information("IP cliente: " + HttpContext.Connection.RemoteIpAddress.ToString());
            }

            ISpanBuilder builder = tracer.BuildSpan("Get::BuscarCentroTrabajo");
            ISpan span = builder.Start();

            try
            {
                span.Log("Inicio: Búsqueda de centro de trabajo por código");
                span.SetTag("Método", "Get:BuscarCentroTrabajo");

                ICentroTrabajoService centroTrabajoService = new CentroTrabajoService(config.GetConnectionString("DefaultConnection"));
                var response = await centroTrabajoService.GetCentroTrabajoByCodigo(codigoCentroTrabajo, activo);

                span.Finish();

                if (environment.Equals(Constante.Desarrollo)) Log.Information("Fin: Método BuscarCentroTrabajo");
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
    }
}
