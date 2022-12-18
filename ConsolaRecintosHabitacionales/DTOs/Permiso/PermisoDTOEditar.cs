using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Permiso
{
    public class PermisoDTOEditar
    {

        public string NombrePermiso { get; set; } = null!;

        public bool Concedido { get; set; }

        public string? CssPermiso { get; set; }
    }
}
