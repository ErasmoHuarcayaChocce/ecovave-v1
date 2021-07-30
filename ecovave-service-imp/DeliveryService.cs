using ecovave.common;
using ecovave.dao.imp;
using ecovave.dao.intf;
using ecovave.model;
using ecovave.service.intf;
using minedu.tecnologia.util.lib;
using minedu.tecnologia.util.lib.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ecovave.service.imp
{
    public class DeliveryService : ServiceBase, IDeliveryService
    {
        private IDeliveryDAO deliveryDAO;
        public DeliveryService(string txtConnectionString)
        {
            base.txtConnectionString = txtConnectionString;
            deliveryDAO = new DeliveryDAO(this.txtConnectionString);
        }

        public async Task<int> CrearDelivery(DeliveryDto request)
        {
            int response = 0;
            TransactionBase transaction = await SqlHelper.BeginTransaction(txtConnectionString);
            try
            {
                Delivery delivery = new Delivery
                {
                    UserId = request.UserId,
                    ScheduleId = request.ScheduleId,
                    QuantityKg = request.QuantityKg,
                    DeliveryDate = request.DeliveryDate,
                    Description = request.Description,
                    IsDeleted = request.IsDeleted,
                    CreatedUser = request.CreatedUser,
                    CreatedDate = request.CreatedDate,
                    CreateIp = request.CreatedIp
                };
                response = await deliveryDAO.CrearDelivery(delivery, transaction);
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
            if (deliveryDAO != null) deliveryDAO = null;
        }

        public Task<int> EliminarDelivery(DeliveryDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeliveryDto> GetDeliveryById(int idDelivery)
        {
            if (idDelivery <= 0) throw new ValidationCustomException(Constante.EX_PARAMETROS_INCORRECTOS);
            DeliveryDto delivery = await deliveryDAO.GetDeliveryById(idDelivery);
            return delivery;
        }

        public async Task<int> ModificarDelivery(DeliveryDto request)
        {
            int response = 0;
            if (request.UserId <= 0) throw new ValidationCustomException(Constante.EX_PARAMETROS_INCORRECTOS);
            TransactionBase transaction = await SqlHelper.BeginTransaction(txtConnectionString);
            try
            {
                Delivery delivery = new Delivery
                {
                    UserId = request.UserId,
                    ScheduleId = request.ScheduleId,
                    QuantityKg = request.QuantityKg,
                    DeliveryDate = request.DeliveryDate,
                    Description = request.Description,
                    IsDeleted = request.IsDeleted,
                    CreatedUser = request.CreatedUser,
                    CreatedDate = request.CreatedDate,
                    CreateIp = request.CreatedIp,
                    ModifiedUser = request.ModifiedUser,
                    ModifiedDate = request.ModifiedDate,
                    ModifiedIp = request.ModifiedIp
                };
                response = await deliveryDAO.ModificarDelivery(delivery, transaction);
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
    }
}
