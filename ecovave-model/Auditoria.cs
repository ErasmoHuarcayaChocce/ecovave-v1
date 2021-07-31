using System;
using System.Collections.Generic;
using System.Text;

namespace ecovave.model
{
    public class Auditoria : AuditoriaModificacion
    {
        public DateTime fechaCreacion { get; set; }
        public string usuarioCreacion { get; set; }
        public string ipCreacion { get; set; }
    }

    public class AuditoriaModificacion
    {
        public DateTime? fechaModificacion { get; set; }
        public string usuarioModificacion { get; set; }
        public string ipModificacion { get; set; }
    }
    public class Audit : AuditUpdate
    {
        public string CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedIp { get; set; }
    }
    public class AuditUpdate
    {
        public string ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedIp { get; set; }
    }
}
