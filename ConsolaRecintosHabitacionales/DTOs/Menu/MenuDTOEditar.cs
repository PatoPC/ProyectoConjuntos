using DTOs.Permiso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Menu
{
    public class MenuDTOEditar
    {
        public string Nombremenu { get; set; } = null!;

        public string Rutamenu { get; set; } = null!;

        public string? Datoicono { get; set; }
        public virtual List<PermisoDTOEditar> Permisos { get; set; } = new List<PermisoDTOEditar>();
    }
}
