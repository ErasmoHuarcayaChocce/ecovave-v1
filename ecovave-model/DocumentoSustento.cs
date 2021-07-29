using System;
using System.Collections.Generic;
using System.Text;

namespace ecovave.model
{
    public class DocumentoSustento : Auditoria
    {
        public int idDocumentoSustento { get; set; }
        public int idLicencia { get; set; }
        public int idTipoDocumentoSustento { get; set; }
        public int idTipoFormatoSustento { get; set; }
        public int idOrigenRegistro { get; set; }
        public string numeroDocumentoSustento { get; set; }
        public string entidadEmisora { get; set; }
        public DateTime fechaEmision { get; set; }
        public int? numeroFolios { get; set; }
        public string sumilla { get; set; }
        public string codigoDocumentoSustento { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public bool eliminado { get; set; }
        public bool vistoProyecto { get; set; }
    }
    
    public class DocumentoSustentoDto
    {
        public int idDocumentoSustento { get; set; }
        public int idLicencia { get; set; }
        public int idTipoDocumentoSustento { get; set; }
        public int idTipoFormatoSustento { get; set; }
        public int idOrigenRegistro { get; set; }        
        public string numeroDocumentoSustento { get; set; }
        public string entidadEmisora { get; set; }
        public string fechaEmision { get; set; }
        public int? numeroFolios { get; set; }
        public string sumilla { get; set; }
        public string codigoDocumentoSustento { get; set; }
        public string fechaRegistro { get; set; }
        public bool vistoProyecto { get; set; }
        // descripciones
        public string descripcionTipoFormato { get; set; }
        public string descripcionTipoSustento { get; set; }
        public int codigoOrigenRegistro { get; set; }
        public  string descripcionVistoProyecto { get; set; }
    }
}
