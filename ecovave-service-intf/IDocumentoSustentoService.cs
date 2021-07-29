using ecovave.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecovave.service.intf
{
    public interface IDocumentoSustentoService
    {
        Task<IEnumerable<DocumentoSustentoDto>> GetDocumentoSustentoByIdLicencia(int idLicencia);
    }
}
