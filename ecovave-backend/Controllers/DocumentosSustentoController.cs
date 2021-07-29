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
    [Route("v1/ecovave/documentossustento")]
    [ApiController]
    public class DocumentosSustentoController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly ITracer tracer;
        private readonly string environment;

        public DocumentosSustentoController(IConfiguration config, ITracer tracer)
        {
            this.config = config;
            this.tracer = tracer;
            this.environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Constante.Produccion;
        }

        [HttpGet()]
        [Produces("application/json", Type = typeof(StatusResponse))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDocumentoSustento([FromQuery] int idLicencia)
        {
            if (environment.Equals(Constante.Desarrollo))
            {
                Log.Information("Inicio: Método GetDocumentoSustento");
                Log.Information("Cadena de conexión: " + config.GetConnectionString("DefaultConnection"));
                Log.Information("IP cliente: " + HttpContext.Connection.RemoteIpAddress.ToString());
            }

            ISpanBuilder builder = tracer.BuildSpan("Get::GetDocumentoSustento");
            ISpan span = builder.Start();

            try
            {
                span.Log("Inicio: Lista de documento sustento");
                span.SetTag("Método", "Get:GetDocumentoSustento");


                IDocumentoSustentoService licencia = new DocumentoSustentoService(config.GetConnectionString("DefaultConnection"));
                IEnumerable<DocumentoSustentoDto> response = await licencia.GetDocumentoSustentoByIdLicencia(idLicencia);

                span.Finish();

                if (environment.Equals(Constante.Desarrollo)) Log.Information("Fin: Método GetDocumentoSustento");
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
