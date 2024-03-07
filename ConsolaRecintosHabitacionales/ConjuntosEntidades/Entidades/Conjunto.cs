using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class Conjunto
    {
        public Conjunto()
        {
            AreaComunals = new HashSet<AreaComunal>();
            Comunicados = new HashSet<Comunicado>();
            ConMsts = new HashSet<ConMst>();
            EncabezadoContabilidads = new HashSet<EncabezadoContabilidad>();
            Parametros = new HashSet<Parametro>();
            Proveedores = new HashSet<Proveedore>();
            Torres = new HashSet<Torre>();
        }

        public Guid IdConjunto { get; set; }
        public string NombreConjunto { get; set; } = null!;
        public string RucConjunto { get; set; } = null!;
        public string DireccionConjunto { get; set; } = null!;
        public string TelefonoConjunto { get; set; } = null!;
        public string MailConjunto { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }

        public virtual ICollection<AreaComunal> AreaComunals { get; set; }
        public virtual ICollection<Comunicado> Comunicados { get; set; }
        public virtual ICollection<ConMst> ConMsts { get; set; }
        public virtual ICollection<EncabezadoContabilidad> EncabezadoContabilidads { get; set; }
        public virtual ICollection<Parametro> Parametros { get; set; }
        public virtual ICollection<Proveedore> Proveedores { get; set; }
        public virtual ICollection<Torre> Torres { get; set; }
    }
}
