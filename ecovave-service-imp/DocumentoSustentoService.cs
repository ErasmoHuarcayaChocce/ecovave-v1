using ecovave.common;
using ecovave.dao.imp;
using ecovave.dao.intf;
using ecovave.model;
using ecovave.service.intf;
using minedu.tecnologia.util.lib;
using minedu.tecnologia.util.lib.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecovave.service.imp
{
    public class DocumentoSustentoService : ServiceBase, IDocumentoSustentoService
    {
        public DocumentoSustentoService(string txtConnectionString)
        {
            base.txtConnectionString = txtConnectionString;
        }

        public async Task<IEnumerable<DocumentoSustentoDto>> GetDocumentoSustentoByIdLicencia(int idLicencia)
        {
            try
            {
                if (idLicencia <= 0)
                {
                    throw new ValidationCustomException(Constante.EX_PARAMETROS_INCORRECTOS);
                }
                IDocumentoSustentoDAO documentoSustentoDAO = new DocumentoSustentoDAO(this.txtConnectionString);
                IEnumerable<DocumentoSustentoDto> response = await documentoSustentoDAO.GetDocumentoSustentoCustomByIdLicencia(idLicencia);
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
