using ecovave.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecovave.dao.intf
{
    public interface IScheduleDAO
    {
        Task<IEnumerable<ScheduleDto>> GetScheduleById(int scheduleId);
        Task<int> CrearSchedule(Schedule request);
        Task<int> ModificarSchedule(Schedule request);
        Task<int> DesactivarSchedule(Schedule request);
    }
}
