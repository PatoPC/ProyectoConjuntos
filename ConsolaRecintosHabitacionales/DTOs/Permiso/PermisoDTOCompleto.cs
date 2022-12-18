using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Permiso
{
    public class PermisoDTOCompleto
    {
        public Guid IdPermisos { get; set; }

        public Guid? IdMenu { get; set; }

        public string NombrePermiso { get; set; } = null!;

        public bool Concedido { get; set; }

        public string? CssPermiso { get; set; }
    }
}
