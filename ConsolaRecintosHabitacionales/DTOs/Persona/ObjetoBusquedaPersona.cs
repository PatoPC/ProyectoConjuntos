using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Persona
{
    public class ObjetoBusquedaPersona
    {
        public Guid? IdPersona { get; set; } = Guid.Empty;
        public string? NombrePersona { get; set; } = null!;
        public string? IdentificacionPersona { get; set; } = null!;       
        public string? EmailPersona { get; set; }
        
    }
}
