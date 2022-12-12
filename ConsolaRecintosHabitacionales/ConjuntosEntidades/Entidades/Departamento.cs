using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class Departamento
    {
        public Departamento()
        {
            TipoPersonas = new HashSet<TipoPersona>();
        }

        public Guid IdDepartamento { get; set; }
        public Guid IdTorres { get; set; }
        public decimal AliqDepartamento { get; set; }
        public string CodigoDepartamento { get; set; } = null!;
        public decimal MetrosDepartamento { get; set; }
        public decimal? SaldoInicialAnual { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        public virtual Torre IdTorresNavigation { get; set; } = null!;
        public virtual ICollection<TipoPersona> TipoPersonas { get; set; }
    }
}
