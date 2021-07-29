using ecovave.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecovave.dao.intf
{
    public interface IAccionDAO
    {
        Task<IEnumerable<Accion>> GetAccionesByGrupo(int? idGrupoAccion, int? idRegimenLaboral, string codigoRolPassport, bool? activo);
        Task<Accion> GetAccionById(int idAccion);
    }
}
