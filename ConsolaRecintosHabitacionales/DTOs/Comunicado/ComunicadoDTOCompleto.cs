using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Comunicado
{
    public class ComunicadoDTOCompleto
    {
        public Guid IdComunicado { get; set; }
        public Guid IdConjunto { get; set; }
        public string NombreConjunto { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }
    }
}
