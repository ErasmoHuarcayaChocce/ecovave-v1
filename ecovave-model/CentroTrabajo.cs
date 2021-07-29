namespace ecovave.model
{
    public class CentroTrabajo : Auditoria
    {
        public int idCentroTrabajo { get; set; }
        public int idTipoCentroTrabajo { get; set; }
        public int idOtraInstancia { get; set; }
        public int idDre { get; set; }
        public int idUgel { get; set; }
        public int idInstitucionEducativa { get; set; }
        public string codigoCentroTrabajo { get; set; }
        public bool? activo { get; set; }
    }

    public class CentroTrabajoFiltro : Paginado
    {
        public int idNivelInstancia { get; set; }
        public int? idInstancia { get; set; }
        public int? idSubinstancia { get; set; }
        public int? idTipoCentroTrabajo { get; set; }
        public string institucionEducativa { get; set; }
    }

    public class CentroTrabajoRegistro
    {
        public int idCentroTrabajo { get; set; }
        public string codigoCentroTrabajo { get; set; }
        public int id { get; set; }
        public string centroTrabajo { get; set; }
        public string instancia { get; set; }
        public string subinstancia { get; set; }
        public int idTipoCentroTrabajo { get; set; }
        public string tipoCentroTrabajo { get; set; }
        public string modalidadEducativa { get; set; }
        public int? idNivelEducativo { get; set; }
        public string nivelEducativo { get; set; }
        public int? idUnidadEjecutora { get; set; }
        public bool tieneEstructuraOrganica { get; set; }
        public int idNivelSede { get; set; }
    }

    public class CentroTrabajoGrilla : CentroTrabajoRegistro
    {
        public int registro { get; set; }
        public int totalRegistro { get; set; }
    }

    public class CentroTrabajoConsulta : Passport
    {
        public string codigoCentroTrabajo { get; set; }
    }

    public class CentroTrabajoReplica : Auditoria
    {
        public string codigoTipoCentroTrabajo { get; set; }
        public string codigoOtraInstancia { get; set; }
        public int idOtraInstancia { get; set; }
        public string codigodDre { get; set; }
        public string codigodUgel { get; set; }
        public string codigoModular { get; set; }
        public string codigoCentroTrabajo { get; set; }
        public bool? activo { get; set; }
    }

    public class CentroTrabajoResponse
    {
        public int idCentroTrabajo { get; set; }
        public int idTipoCentroTrabajo { get; set; }
        public int idOtraInstancia { get; set; }
        public int idDre { get; set; }
        public int idUgel { get; set; }
        public int idInstitucionEducativa { get; set; }
        public string codigoCentroTrabajo { get; set; }
    }

}
