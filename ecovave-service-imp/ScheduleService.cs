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
    public class ScheduleService : ServiceBase, IScheduleService
    {
        public ScheduleService(string txtConnectionString)
        {
            base.txtConnectionString = txtConnectionString;
        }

        public async Task<int> CrearSchedule(ScheduleDto request)
        {
            int response = 0;
            try
            {


                Schedule schedule = new Schedule
                {
                    UserId = request.UserId,
                    RecyclingTypeId = request.RecyclingTypeId,
                    QuantityKg = request.QuantityKg,
                    ScheduleDate = request.ScheduleDate,
                    StatusId = request.StatusId,
                    EsDeleted = request.EsDeleted,
                    CreatedUser = request.CreatedUser,
                    CreatedDate = request.CreatedDate,
                    CreatedIp = request.CreatedIp
                };

                IScheduleDAO scheduleDAO = new ScheduleDAO(txtConnectionString);
                response = await scheduleDAO.CrearSchedule(schedule);
                if (response == 0) throw new ValidationCustomException(Constante.EX_DRE_CREATE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }


        public Task<int> DesactivarSchedule(ScheduleDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ScheduleDto>> GetScheduleById(int scheduleId)
        {
            try
            {
                IScheduleDAO scheduleDAO = new ScheduleDAO(txtConnectionString);
                IEnumerable<ScheduleDto> response = await scheduleDAO.GetScheduleById(scheduleId);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> ModificarSchedule(ScheduleDto request)
        {
            int response = 0;
            try
            {


                Schedule schedule = new Schedule
                {
                    UserId = request.UserId,
                    RecyclingTypeId = request.RecyclingTypeId,
                    QuantityKg = request.QuantityKg,
                    ScheduleDate = request.ScheduleDate,
                    StatusId = request.StatusId,
                    EsDeleted = request.EsDeleted,
                    ModifiedUser = request.ModifiedUser,
                    ModifiedDate = request.ModifiedDate,
                    ModifiedIp = request.ModifiedIp
                };

                IScheduleDAO scheduleDAO = new ScheduleDAO(txtConnectionString);
                response = await scheduleDAO.ModificarSchedule(schedule);
                if (response == 0) throw new ValidationCustomException(Constante.EX_DRE_CREATE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}
