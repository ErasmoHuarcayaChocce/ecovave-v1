using ecovave.common;
using ecovave.model;
using ecovave.service.imp.Maestros;
using ecovave.service.intf.Maestros;
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

namespace ecovave.backend.Controllers.Maestros
{
    [Route("v1/ecovave/acciones")]
    [ApiController]
    public class AccionesController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly ITracer tracer;
        private readonly string environment;

        public AccionesController(IConfiguration config, ITracer tracer)
        {
            this.config = config;
            this.tracer = tracer;
            this.environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Constante.Produccion;
        }


        [HttpGet("", Name = "Acciones por Id")]
        [Produces("application/json", Type = typeof(StatusResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAccionByGrupo([FromQuery] int? idGrupoAccion, int? idRegimenLaboral, string codigoRolPassport, bool? activo)
        {
            if (environment.Equals(Constante.Desarrollo))
            {
                Log.Information("Inicio: Método GetAccionByGrupo");
                Log.Information("Cadena de conexión: " + config.GetConnectionString("DefaultConnection"));
                Log.Information("IP cliente: " + HttpContext.Connection.RemoteIpAddress.ToString());
            }

            ISpanBuilder builder = tracer.BuildSpan("Get: GetAccionByGrupo");
            ISpan span = builder.Start();

            try
            {

                span.Log("Inicio: Obtener grupo acciones");
                span.SetTag("Método", "Get:GetAccionByGrupo");
                IAccionService accion = new AccionService(config.GetConnectionString("DefaultConnection"));
                IEnumerable<Accion> response = await accion.GetAccionesByGrupo(idGrupoAccion, idRegimenLaboral, codigoRolPassport, activo);

                span.Finish();

                if (environment.Equals(Constante.Desarrollo)) Log.Information("Fin: Método GetServidorPublicoById");
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
