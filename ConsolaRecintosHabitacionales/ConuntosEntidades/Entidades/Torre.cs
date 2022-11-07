using System;
using System.Collections.Generic;

namespace ConuntosEntidades.Entidades
{
    public partial class Torre
    {
        public Torre()
        {
            Departamentos = new HashSet<Departamento>();
        }

        public Guid IdTorres { get; set; }
        public Guid IdConjunto { get; set; }
        public string NombreTorres { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }

        public virtual Conjunto? IdConjuntoNavigation { get; set; }
        public virtual ICollection<Departamento> Departamentos { get; set; }
    }
}
