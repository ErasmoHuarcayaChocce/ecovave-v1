using ecovave.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecovave.dao.intf
{
    public interface ICentroTrabajoDAO
    {
        Task<IEnumerable<CentroTrabajoGrilla>> ListaCentroTrabajo(CentroTrabajoFiltro request);
        Task<IEnumerable<CentroTrabajoGrilla>> ListaCentroTrabajoDREUGEL(CentroTrabajoFiltro request);
        Task<CentroTrabajoRegistro> GetCentroTrabajo(CentroTrabajoConsulta request);
        Task<int> GetValidarCentroTrabajo(string codigoCentroTrabajo);
        Task<CentroTrabajoResponse> GetCentroTrabajoByCodigo(string codigoCentroTrabajo, bool? activo);
        Task<int> CrearCentroTrabajoReplica(CentroTrabajo request);
        Task<int> ModificarCentroTrabajoReplica(CentroTrabajo request);
        Task<int> DesactivarCentroTrabajoReplica(CentroTrabajo request);
    }
}
