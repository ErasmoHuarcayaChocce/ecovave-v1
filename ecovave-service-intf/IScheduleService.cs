using ecovave.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecovave.service.intf
{
    public   interface IScheduleService
    {
        Task<IEnumerable<ScheduleDto>> GetScheduleById(int scheduleId);
        Task<int> CrearSchedule(ScheduleDto request);
        Task<int> ModificarSchedule(ScheduleDto request);
        Task<int> DesactivarSchedule(ScheduleDto request);
    }
}
