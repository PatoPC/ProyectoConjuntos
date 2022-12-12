using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Persona
{
    public class PersonaDTOCompleto
    {
        public Guid IdPersona { get; set; }
        public string NombrePersona { get; set; } = null!;
        public string IdentificacionPersona { get; set; } = null!;
        public string? TelefonoPersona { get; set; }
        public string? EmailPersona { get; set; }
        public string? ObservacionPersona { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; }
    }
}
