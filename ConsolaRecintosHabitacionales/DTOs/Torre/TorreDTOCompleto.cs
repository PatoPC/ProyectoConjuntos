using DTOs.Departamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Torre
{
    public class TorreDTOCompleto
    {
        public Guid IdTorres { get; set; }
        public Guid IdConjunto { get; set; }
        public string NombreConjunto { get; set; } = null!;
        public string NombreTorres { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }
        public List<DepartamentoDTOCompleto> Departamentos { get; set; } = null;
    }
}
