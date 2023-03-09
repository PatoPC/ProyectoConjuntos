using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class ConMst
    {
        public Guid IdConMst { get; set; }
        public string CuentaCon { get; set; } = null!;
        public string NombreCuenta { get; set; } = null!;
        public bool Grupo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }
    }
}
