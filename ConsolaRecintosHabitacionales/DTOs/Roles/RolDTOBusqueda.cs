using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Roles
{
    public class RolDTOBusqueda
    {

        public Guid IdRol { get; set; }
        public string? NombreRol { get; set; }        
        public Guid? IdPaginaInicioRol { get; set; }
        public bool RolRestringido { get; set; }
        public bool Estado { get; set; }

        public List<ModuloBusquedaDTO>? Modulos { get; set; }
    }

    public class ModuloBusquedaDTO
    {
        public string? nombreModulo { get; set; }

    }

}
