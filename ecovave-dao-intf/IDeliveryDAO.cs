using ecovave.model;
using minedu.tecnologia.util.lib;
using System.Threading.Tasks;

namespace ecovave.dao.intf
{
    public interface IDeliveryDAO
    {
        Task<DeliveryDto> GetDeliveryById(int idDelivery);
        Task<int> CrearDelivery(Delivery request, TransactionBase transaction);
        Task<int> ModificarDelivery(Delivery request, TransactionBase transaction);
        Task<int> EliminarDelivery(Delivery request, TransactionBase transaction);
    }
}
