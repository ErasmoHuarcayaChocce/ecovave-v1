using ecovave.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecovave.service.intf
{
    public interface ICentroTrabajoService
    {
        Task<IEnumerable<CentroTrabajoGrilla>> ListaCentroTrabajo(CentroTrabajoFiltro request);
        Task<CentroTrabajoRegistro> GetCentroTrabajo(CentroTrabajoConsulta request);
        Task<CentroTrabajoResponse> GetCentroTrabajoByCodigo(string codigoCentroTrabajo, bool? activo);
        Task<int> CrearCentroTrabajoReplica(CentroTrabajoReplica request);
        Task<int> ModificarCentroTrabajoReplica(CentroTrabajoReplica request);
        Task<int> DesactivarCentroTrabajoReplica(CentroTrabajoReplica request);
    }
}
