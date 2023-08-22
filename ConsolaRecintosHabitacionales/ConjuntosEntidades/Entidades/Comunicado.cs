using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class Comunicado
    {
        public Guid IdComunicado { get; set; }
        public Guid IdConjunto { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }

        public virtual Conjunto IdConjuntoNavigation { get; set; } = null!;
    }
}
