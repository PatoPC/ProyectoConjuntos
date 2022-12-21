using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Persona
{
    public class PersonaDTOCrear
    {
        public Guid IdTipoIdentificacion { get; set; }
        public string NombresPersona { get; set; } = null!;
        public string? ApellidosPersona { get; set; }
        public string IdentificacionPersona { get; set; } = null!;
        public string? TelefonoPersona { get; set; }
        public string? CelularPersona { get; set; }
        public string? EmailPersona { get; set; }
        public string? ObservacionPersona { get; set; }       
        public string? UsuarioCreacion { get; set; } = null!;
    }
}
