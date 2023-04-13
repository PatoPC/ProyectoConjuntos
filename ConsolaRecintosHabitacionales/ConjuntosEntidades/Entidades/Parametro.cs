using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class Parametro
    {
        public Guid IdParametro { get; set; }
        public Guid IdConjunto { get; set; }
        public Guid IdCatParametro { get; set; }
        public string? CtaCont1 { get; set; }
        public byte[]? CtaCont2 { get; set; }
        public string? CtaCont3 { get; set; }
        public string? CtaCont4 { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;

        public virtual Conjunto IdConjuntoNavigation { get; set; } = null!;
    }
}
