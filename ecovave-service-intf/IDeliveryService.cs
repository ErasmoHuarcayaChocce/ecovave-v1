using ecovave.model;
using System;
using System.Threading.Tasks;

namespace ecovave.service.intf
{
    public interface IDeliveryService : IDisposable
    {
        Task<DeliveryDto> GetDeliveryById(int idDelivery);
        Task<int> CrearDelivery(DeliveryDto request);
        Task<int> ModificarDelivery(DeliveryDto request);
        Task<int> EliminarDelivery(DeliveryDto request);
    }
}
