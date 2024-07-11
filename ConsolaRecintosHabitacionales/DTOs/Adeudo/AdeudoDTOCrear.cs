using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Adeudo
{
    public class AdeudoDTOCrear
    {
        public string? NombreConjunto { get; set; } = null;
        public string? Departamento { get; set; } = null;
        public Guid IdDepartamento { get; set; }
        public Guid IdPersona { get; set; }
		public Guid IdCuentaDebe { get; set; }
		public Guid IdCuentaHaber { get; set; }
		public DateTime FechaAdeudos { get; set; }
		public string NombreCuentaDebe { get; set; } = null!;
		public string NombreCuentaHaber { get; set; } = null!;
		public string Torre { get; set; } = null!;
        public string? NombresPersona { get; set; } = null!;
        public string? ApellidosPersona { get; set; } = null!;
        public decimal MontoAdeudos { get; set; }
        public bool EstadoAdeudos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
       
    }
}
