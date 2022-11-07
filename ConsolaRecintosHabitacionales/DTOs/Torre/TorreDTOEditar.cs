using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Torre
{
    public class TorreDTOEditar
    {
        public Guid IdTorresEditar { get; set; }
        public Guid IdConjuntoEditar { get; set; }
        public string NombreTorresEditar { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }
    }
}
