using ecovave.model;
using System;
using System.Threading.Tasks;

namespace ecovave.service.intf
{
    public interface IUserService : IDisposable
    {
        Task<UserResponse> GetUsuarioCustomById(int idUsuarrio);
        Task<int> CrearUsuario(UserDto request);
        Task<int> ModificarUsuario(UserDto request);
        Task<int> EliminarUsuario(UserDto request);
    }
}
