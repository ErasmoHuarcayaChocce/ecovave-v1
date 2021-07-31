using ecovave.common;
using ecovave.dao.imp;
using ecovave.dao.intf;
using ecovave.model;
using ecovave.service.intf;
using minedu.tecnologia.util.lib;
using minedu.tecnologia.util.lib.Exceptions;
using System;
using System.Threading.Tasks;

namespace ecovave.service.imp
{
    public class UserService : ServiceBase, IUserService
    {
        private IUserDAO userDAO;
        public UserService(string txtConnectionString)
        {
            base.txtConnectionString = txtConnectionString;
            userDAO = new UserDAO(this.txtConnectionString);
        }
        public async Task<int> CrearUsuario(UserDto request)
        {
            int response = 0;
            TransactionBase transaction = await SqlHelper.BeginTransaction(txtConnectionString);
            try
            {
                User user = new User
                {
                    UserName = request.UserName,
                    FirsName = request.FirsName,
                    LastName = request.LastName,
                    DocumentTypeId = request.DocumentTypeId,
                    DocumentNumber = request.DocumentNumber,
                    MobilphoneNumber = request.MobilphoneNumber,
                    TelephoneNumber = request.TelephoneNumber,
                    IsActived = request.IsActived,
                    EsDeleted = request.EsDeleted,
                    CreatedUser = request.CreatedUser,
                    CreatedDate = request.CreatedDate,
                    CreatedIp = request.CreateIp
                };
                response = await userDAO.CrearUsuario(user, transaction);
                if (response < 1) throw new CustomException(Constante.EX_DRE_CREATE);
            }
            catch (Exception ex)
            {
                await SqlHelper.RollbackTransactionAsync(transaction);
                throw ex;
            }
            finally
            {
                await SqlHelper.DisposeTransactionAsync(transaction);
            }
            return response;
        }
        public Task<int> EliminarUsuario(UserDto request)
        {
            throw new NotImplementedException();
        }
        public async Task<int> ModificarUsuario(UserDto request)
        {
            int response = 0;
            if (request.UserId <= 0) throw new ValidationCustomException(Constante.EX_PARAMETROS_INCORRECTOS);
            TransactionBase transaction = await SqlHelper.BeginTransaction(txtConnectionString);
            try
            {
                User user = new User
                {
                    UserId = request.UserId,
                    UserName = request.UserName,
                    FirsName = request.FirsName,
                    LastName = request.LastName,
                    DocumentTypeId = request.DocumentTypeId,
                    DocumentNumber = request.DocumentNumber,
                    MobilphoneNumber = request.MobilphoneNumber,
                    TelephoneNumber = request.TelephoneNumber,
                    IsActived = request.IsActived,
                    ModifiedUser = request.ModifiedUser,
                    ModifiedDate = request.ModifiedDate,
                    ModifiedIp = request.ModifiedIp
                };
                response = await userDAO.ModificarUsuario(user, transaction);
                if (response < 1) throw new CustomException(Constante.EX_DRE_CREATE);
            }
            catch (Exception ex)
            {
                await SqlHelper.RollbackTransactionAsync(transaction);
                throw ex;
            }
            finally
            {
                await SqlHelper.DisposeTransactionAsync(transaction);
            }
            return response;
        }
        public void Dispose()
        {
            if (userDAO != null) userDAO = null;
        }
        public async Task<UserResponse> GetUsuarioCustomById(int idUsuarrio)
        {
            if (idUsuarrio <= 0) throw new ValidationCustomException(Constante.EX_PARAMETROS_INCORRECTOS);
            UserResponse usuarioResponse = await userDAO.GetUsuarioCustomById(idUsuarrio);
            return usuarioResponse;
        }
    }
}
