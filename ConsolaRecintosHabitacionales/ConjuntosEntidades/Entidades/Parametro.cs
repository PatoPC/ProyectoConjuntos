using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class Parametro
    {
        public Guid IdParametro { get; set; }
        public Guid IdConjunto { get; set; }
        public string NombreParametro { get; set; } = null!;
        public Guid CtaCont1 { get; set; }
        public Guid? CtaCont2 { get; set; }
        public Guid? CtaCont3 { get; set; }
        public Guid? CtaCont4 { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public Guid? IdModulo { get; set; }

        public virtual Conjunto IdConjuntoNavigation { get; set; } = null!;
    }
}
