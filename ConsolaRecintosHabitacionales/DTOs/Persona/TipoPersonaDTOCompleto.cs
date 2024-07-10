using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Persona
{
    public class TipoPersonaDTOCompleto
    {
        public Guid? IdPersona { get; set; }
        public Guid? IdDepartamento { get; set; }
        public Guid? IdTipoPersonaDepartamento { get; set; }
        public string? CodigoDepartamento { get; set; } = null!;
        public string? TipoPersona { get; set; } = null!;
        public string? ConjuntoPersona { get; set; } = null!;
        public string? NombrePersona { get; set; } = null!;
        public string? UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }
    }
}
