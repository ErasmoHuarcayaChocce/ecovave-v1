using ecovave.common;
using ecovave.dao.imp;
using ecovave.dao.intf;
using ecovave.model;
using ecovave.service.intf.Maestros;
using minedu.tecnologia.util.lib;
using minedu.tecnologia.util.lib.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecovave.service.imp.Maestros
{
    public class AccionService : ServiceBase, IAccionService
    {
        public AccionService(string txtConnectionString)
        {
            base.txtConnectionString = txtConnectionString;
        }

        public async Task<IEnumerable<Accion>> GetAccionesByGrupo(int? idGrupoAccion, int? idRegimenLaboral, string codigoRolPassport, bool? activo)
        {
            try
            {
                if (idGrupoAccion <= 0 || idRegimenLaboral <= 0)
                    throw new ValidationCustomException(Constante.EX_PARAMETROS_INCORRECTOS);

                IAccionDAO accionDAO = new AccionDAO(this.txtConnectionString);
                IEnumerable<Accion> response = await accionDAO.GetAccionesByGrupo(idGrupoAccion, idRegimenLaboral, codigoRolPassport, activo);
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
