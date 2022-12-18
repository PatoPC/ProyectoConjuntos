using DTOs.Modulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Roles
{
    public class RolDTOEditar
    {
        public string? NombreRol { get; set; } = null!;

        public bool Estado { get; set; }

        public Guid? IdPaginaInicioRol { get; set; }

        public string? UsuarioModificacion { get; set; } = null!;

        public bool AccesoTodos { get; set; }

        public bool RolRestringido { get; set; }

        public List<ModuloDTOEditar> listaModulos { get; set; } = new List<ModuloDTOEditar>();

    }
}
