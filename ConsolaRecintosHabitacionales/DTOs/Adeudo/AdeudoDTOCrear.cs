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
        public DateTime FechaAdeudos { get; set; }
        public string? NombresPersona { get; set; } = null!;
        public string? ApellidosPersona { get; set; } = null!;
        public decimal MontoAdeudos { get; set; }
        public bool EstadoAdeudos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
       
    }
}
