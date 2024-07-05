using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class Persona
    {
        public Persona()
        {
            Adeudos = new HashSet<Adeudo>();
            TipoPersonas = new HashSet<TipoPersona>();
        }

        public Guid IdPersona { get; set; }
        public Guid IdTipoIdentificacion { get; set; }
        public string NombresPersona { get; set; } = null!;
        public string? ApellidosPersona { get; set; }
        public string IdentificacionPersona { get; set; } = null!;
        public string? TelefonoPersona { get; set; }
        public string? CelularPersona { get; set; }
        public string? EmailPersona { get; set; }
        public string? ObservacionPersona { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;

        public virtual ICollection<Adeudo> Adeudos { get; set; }
        public virtual ICollection<TipoPersona> TipoPersonas { get; set; }
    }
}
