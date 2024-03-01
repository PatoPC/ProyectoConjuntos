using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class ReservaArea
    {
        public Guid IdReservaArea { get; set; }
        public Guid IdAreaComunal { get; set; }
        public Guid IdPersona { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }

        public virtual AreaComunal IdAreaComunalNavigation { get; set; } = null!;
    }
}
