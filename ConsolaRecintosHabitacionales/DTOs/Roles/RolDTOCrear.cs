using DTOs.Modulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Roles
{
    public class RolDTOCrear
    {        
        public string? NombreRol { get; set; } = null!;

        public bool Estado { get; set; }

        public Guid? IdPaginaInicioRol { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public bool AccesoTodos { get; set; }

        public bool RolRestringido { get; set; }

        public List<ModuloDTOCrear> listaModulos { get; set; } = new List<ModuloDTOCrear>();

        
    }
}
