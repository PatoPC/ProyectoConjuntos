using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Persona
{
    public class PersonaDTOConjunto
    {
        public Guid IdPersona { get; set; }
        public Guid IdTipoIdentificacion { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdConjunto { get; set; }
        public Guid IdTorres { get; set; }
        public Guid IdDepartamento { get; set; }
        public Guid IdTipoPersonaDepartamento { get; set; }
        public string? NombresPersona { get; set; } = null!;
        public string? ApellidosPersona { get; set; }
        public string IdentificacionPersona { get; set; } = null!;
        public string? TelefonoPersona { get; set; }
        public string? EmailPersona { get; set; }
       
    }
}
