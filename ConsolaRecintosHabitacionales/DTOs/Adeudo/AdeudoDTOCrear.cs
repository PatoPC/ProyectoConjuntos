using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Adeudo
{
    public class AdeudoDTOCrear
    {
        public Guid IdDepartamento { get; set; }
        public Guid IdPersona { get; set; }
        public DateTime FechaAdeudos { get; set; }
        public decimal MontoAdeudos { get; set; }
        public bool EstadoAdeudos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
       
    }
}
