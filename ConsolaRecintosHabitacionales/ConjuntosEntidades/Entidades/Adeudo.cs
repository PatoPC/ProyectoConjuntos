using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class Adeudo
    {
        public Guid IdAdeudos { get; set; }
        public Guid IdDepartamento { get; set; }
        public Guid IdPersona { get; set; }
        public DateTime FechaAdeudos { get; set; }
        public decimal MontoAdeudos { get; set; }
        public decimal SaldoPendiente { get; set; }
        public bool EstadoAdeudos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public Guid IdCuentaDebe { get; set; }
        public Guid IdCuentaHaber { get; set; }
        public string NombreCuentaDebe { get; set; } = null!;
        public string NombreCuentaHaber { get; set; } = null!;

        public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
        public virtual Persona IdPersonaNavigation { get; set; } = null!;
    }
}
