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
using System.Threading.Tasks;

namespace ecovave.backend.Controllers
{
    [Route("api/ecovave/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly ITracer tracer;
        private readonly string environment;

        public UserController(IConfiguration config, ITracer tracer)
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
        public async Task<IActionResult> GetUsuarioById([FromRoute] int userId)
        {
            if (environment.Equals(Constante.Desarrollo))
            {
                Log.Information("Inicio: Método GetUsuarioById");
                Log.Information("Cadena de conexión: " + config.GetConnectionString("DefaultConnection"));
                Log.Information("IP cliente: " + HttpContext.Connection.RemoteIpAddress.ToString());
            }

            ISpanBuilder builder = tracer.BuildSpan("Get: GetUsuarioById");
            ISpan span = builder.Start();

            try
            {

                span.Log("Inicio: Obtener usuario");
                span.SetTag("Método", "Get:GetUsuarioById");
                IUserService postulacionService = new UserService(config.GetConnectionString("DefaultConnection"));
                UserResponse response = await postulacionService.GetUsuarioCustomById(userId);

                span.Finish();

                if (environment.Equals(Constante.Desarrollo)) Log.Information("Fin: Método GetUsuarioById");
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
        public async Task<IActionResult> CrearUsuario([FromBody] UserDto request)
        {
            if (environment.Equals(Constante.Desarrollo))
            {
                Log.Information("Inicio: Método CrearUsuario");
                Log.Information("Cadena de conexión: " + config.GetConnectionString("DefaultConnection"));
                Log.Information("IP cliente: " + HttpContext.Connection.RemoteIpAddress.ToString());
            }

            ISpanBuilder builder = tracer.BuildSpan("Post::CrearUsuario");
            ISpan span = builder.Start();

            try
            {
                span.Log("Inicio: usuario");
                span.SetTag("Método", "Set:CrearUsuario");
                string ipRegistro = HttpContext.Connection.RemoteIpAddress.ToString();
                request.CreateIp = ipRegistro;
                int response = 0;
                using (IUserService userService = new UserService(config.GetConnectionString("DefaultConnection")))
                {
                    response = await userService.CrearUsuario(request);
                }
                span.Finish();
                if (environment.Equals(Constante.Desarrollo)) Log.Information("Fin: Método CrearUsuario");
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
        public async Task<IActionResult> ModificarUsuario([FromBody] UserDto request)
        {
            if (environment.Equals(Constante.Desarrollo))
            {
                Log.Information("Inicio: Método ModificarUsuario");
                Log.Information("Cadena de conexión: " + config.GetConnectionString("DefaultConnection"));
                Log.Information("IP cliente: " + HttpContext.Connection.RemoteIpAddress.ToString());
            }

            ISpanBuilder builder = tracer.BuildSpan("Get::ModificarUsuario");
            ISpan span = builder.Start();

            try
            {
                span.Log("Inicio: Modificar la usuario");
                span.SetTag("Método", "Get:ModificarUsuario");
                string ipRegistro = HttpContext.Connection.RemoteIpAddress.ToString();
                request.CreateIp = ipRegistro;

                int response = 0;
                using (IUserService userService = new UserService(config.GetConnectionString("DefaultConnection")))
                {
                    response = await userService.ModificarUsuario(request);
                }

                span.Finish();

                if (environment.Equals(Constante.Desarrollo)) Log.Information("Fin: Método ModificarUsuario");
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
