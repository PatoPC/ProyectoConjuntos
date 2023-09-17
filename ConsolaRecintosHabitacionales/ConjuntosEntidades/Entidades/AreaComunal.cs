using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class AreaComunal
    {
        public AreaComunal()
        {
            ReservaAreas = new HashSet<ReservaArea>();
        }

        public Guid IdAreaComunal { get; set; }
        public Guid IdConjunto { get; set; }
        public string NombreArea { get; set; } = null!;
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public string? Observacion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;

        public virtual Conjunto IdConjuntoNavigation { get; set; } = null!;
        public virtual ICollection<ReservaArea> ReservaAreas { get; set; }
    }
}
