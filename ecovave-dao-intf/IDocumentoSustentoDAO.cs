using ecovave.model;
using minedu.tecnologia.util.lib;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecovave.dao.intf
{
    public interface IDocumentoSustentoDAO
    {
        Task<int> CrearDocumentoSustento(DocumentoSustento request, TransactionBase transaction);

        Task<IEnumerable<DocumentoSustentoDto>> GetDocumentoSustentoCustomByIdLicencia(int idLicencia);        
        Task<int> ModificarDocumentoSustento(DocumentoSustento request, TransactionBase transaction);
        Task<int> EliminarDocumentoSustento(DocumentoSustento request, TransactionBase transaction);
    }
}
