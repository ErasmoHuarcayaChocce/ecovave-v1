using ecovave.model;
using minedu.tecnologia.util.lib;
using System.Threading.Tasks;

namespace ecovave.dao.intf
{
    public interface IUserDAO
    {
        Task<UserResponse> GetUsuarioCustomById(int idUsuarrio);
        Task<int> CrearUsuario(User request, TransactionBase transaction);
        Task<int> ModificarUsuario(User request, TransactionBase transaction);
        Task<int> EliminarUsuario(User request, TransactionBase transaction);
    }
}
