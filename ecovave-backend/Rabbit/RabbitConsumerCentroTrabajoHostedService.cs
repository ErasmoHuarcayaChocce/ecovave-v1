using ecovave.common;
using ecovave.model;
using ecovave.service.imp;
using ecovave.service.intf;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ObjectPool;
using minedu.rrhh.negocio.comunes.rabbitmq.lib;
using minedu.rrhh.negocio.comunes.rabbitmq.lib.ReplicaRegistro;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ecovave.backend.Rabbit
{
    public class RabbitConsumerCentroTrabajoHostedService : BackgroundService // OK
    {
        private readonly DefaultObjectPool<IConnection> pool;
        private IConnection connection;
        private IModel channel;
        private readonly IConfiguration config;
        public RabbitConsumerCentroTrabajoHostedService(IPooledObjectPolicy<IConnection> objectPolicy, IConfiguration configuration)
        {
            config = configuration;
            pool = new DefaultObjectPool<IConnection>(objectPolicy, Environment.ProcessorCount);
            connection = pool.Get();
            channel = RabbitConsumerAyni.InitConsumidorDeMensajesReplica(this, pool, Constante.RABBIT_EXCHANGE_NEGOCIO_COMUNES_MAESTROS_CENTROTRABAJO, Constante.RABBIT_QUEUE_NEGOCIO_COMUNES_MAESTROS_CENTROTRABAJO);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumerCentroTrabajo = new EventingBasicConsumer(channel);
            consumerCentroTrabajo.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                int resultado = ConsumirMensaje(content);
                if (resultado > 0)
                    channel.BasicAck(ea.DeliveryTag, false);
                else
                    channel.BasicNack(ea.DeliveryTag, false, true);
            };

            channel.BasicConsume(Constante.RABBIT_QUEUE_NEGOCIO_COMUNES_MAESTROS_CENTROTRABAJO, false, consumerCentroTrabajo);
            return Task.CompletedTask;
        }

        private int ConsumirMensaje(string contentJson)
        {
            if (string.IsNullOrEmpty(contentJson))
                return 1;
            try
            {
                var result = 0;
                var request = JsonSerializer.Deserialize<MensajeReplicaRegistro<CentroTrabajoReplica>>(contentJson);
                ICentroTrabajoService service = new CentroTrabajoService(config.GetConnectionString("DefaultConnection"));
                if (request.operacion.Equals(TipoEventoReplicaRegistro.CREACION)) result = service.CrearCentroTrabajoReplica(request.entidad).Result;
                if (request.operacion.Equals(TipoEventoReplicaRegistro.ACTUALIZACION)) result = service.ModificarCentroTrabajoReplica(request.entidad).Result;
                if (request.operacion.Equals(TipoEventoReplicaRegistro.ELIMINACION)) result = service.DesactivarCentroTrabajoReplica(request.entidad).Result;
                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"Error al interpretar el mensaje recibido centro trabajo {ex.Message}");
                return 0;
            }
        }

        public override void Dispose()
        {
            RabbitConsumerAyni.CerrarChannel(channel);
            RabbitConsumerAyni.CerrarConnection(connection);
            base.Dispose();
        }
    }
}
