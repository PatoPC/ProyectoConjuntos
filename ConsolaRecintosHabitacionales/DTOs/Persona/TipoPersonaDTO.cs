using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Persona
{
    public class TipoPersonaDTO
    {
        public Guid? IdPersona { get; set; }
        public Guid? IdDepartamento { get; set; }
        public Guid? IdTipoPersonaDepartamento { get; set; }
        public string? CodigoDepartamento { get; set; } = null!;
        public string? TipoPersona { get; set; } = null!;
        public string? NombrePersona { get; set; } = null!;
        public string? UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public PersonaDTOCompleto? IdPersonaNavigation { get; set; } = null!;
    }
}
