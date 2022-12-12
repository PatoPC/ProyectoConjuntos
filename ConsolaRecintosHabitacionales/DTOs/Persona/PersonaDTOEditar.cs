using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Persona
{
    public class PersonaDTOEditar
    {       
        public string NombrePersona { get; set; } = null!;
        public string IdentificacionPersona { get; set; } = null!;
        public string? TelefonoPersona { get; set; }
        public string? EmailPersona { get; set; }
        public string? ObservacionPersona { get; set; }        
        public string? UsuarioModificacion { get; set; }
    }
}
