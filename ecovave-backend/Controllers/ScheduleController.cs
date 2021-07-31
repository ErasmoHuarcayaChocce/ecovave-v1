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
    [Route("api/v1/ecovave/schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly ITracer tracer;
        private readonly string environment;

        public ScheduleController(IConfiguration config, ITracer tracer)
        {
            this.config = config;
            this.tracer = tracer;
            this.environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Constante.Produccion;
        }

        [HttpGet("{scheduleId}", Name = "schedule por Id")]
        [Produces("application/json", Type = typeof(StatusResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetScheduleById([FromRoute] int scheduleId)
        {
            if (environment.Equals(Constante.Desarrollo))
            {
                Log.Information("Inicio: Método GetScheduleById");
                Log.Information("Cadena de conexión: " + config.GetConnectionString("DefaultConnection"));
                Log.Information("IP cliente: " + HttpContext.Connection.RemoteIpAddress.ToString());
            }

            ISpanBuilder builder = tracer.BuildSpan("Get: GetScheduleById");
            ISpan span = builder.Start();

            try
            {
                span.Log("Inicio: Obtener schedule");
                span.SetTag("Método", "Get:GetScheduleById");
                IScheduleService scheduleService = new ScheduleService(config.GetConnectionString("DefaultConnection"));
                IEnumerable<ScheduleDto> response = await scheduleService.GetScheduleById(scheduleId);

                span.Finish();

                if (environment.Equals(Constante.Desarrollo)) Log.Information("Fin: Método GetScheduleById");
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
        public async Task<IActionResult> CrearSchedule([FromBody] ScheduleDto request)
        {
            if (environment.Equals(Constante.Desarrollo))
            {
                Log.Information("Inicio: Método CrearSchedule");
                Log.Information("Cadena de conexión: " + config.GetConnectionString("DefaultConnection"));
                Log.Information("IP cliente: " + HttpContext.Connection.RemoteIpAddress.ToString());
            }

            ISpanBuilder builder = tracer.BuildSpan("Post::CrearSchedule");
            ISpan span = builder.Start();

            try
            {
                span.Log("Inicio: schedule");
                span.SetTag("Método", "Set:CrearSchedule");
                string ipRegistro = HttpContext.Connection.RemoteIpAddress.ToString();
                request.CreatedIp = ipRegistro;
                int response = 0;
                IScheduleService scheduleService = new ScheduleService(config.GetConnectionString("DefaultConnection"));
                response = await scheduleService.CrearSchedule(request);

                span.Finish();
                if (environment.Equals(Constante.Desarrollo)) Log.Information("Fin: Método CrearSchedule");
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
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModificarSchedule([FromBody] ScheduleDto request)
        {
            if (environment.Equals(Constante.Desarrollo))
            {
                Log.Information("Inicio: Método ModificarSchedule");
                Log.Information("Cadena de conexión: " + config.GetConnectionString("DefaultConnection"));
                Log.Information("IP cliente: " + HttpContext.Connection.RemoteIpAddress.ToString());
            }

            ISpanBuilder builder = tracer.BuildSpan("Post::ModificarSchedule");
            ISpan span = builder.Start();

            try
            {
                span.Log("Inicio:modificar schedule");
                span.SetTag("Método", "Set:ModificarSchedule");
                string ipRegistro = HttpContext.Connection.RemoteIpAddress.ToString();
                request.ModifiedIp = ipRegistro;
                int response = 0;
                IScheduleService scheduleService = new ScheduleService(config.GetConnectionString("DefaultConnection"));
                response = await scheduleService.ModificarSchedule(request);

                span.Finish();
                if (environment.Equals(Constante.Desarrollo)) Log.Information("Fin: Método ModificarSchedule");
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
