using DTOs.Permiso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Menu
{
    public class MenuDTOCompleto
    {
        public Guid IdMenu { get; set; }

        public Guid? IdModulo { get; set; }

        public string NombreMenu { get; set; } = null!;

        public string RutaMenu { get; set; } = null!;

        public string? Datoicono { get; set; }
        public virtual List<PermisoDTOCompleto> Permisos { get; set; } = new List<PermisoDTOCompleto>();
    }
}
