using ecovave.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecovave.service.intf.Maestros
{
    public interface IAccionService
    {
        Task<IEnumerable<Accion>> GetAccionesByGrupo(int? idGrupoAccion, int? idRegimenLaboral, string codigoRolPassport, bool? activo);
    }
}
