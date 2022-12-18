using DTOs.Modulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Roles
{
    public class RolDTOCompleto
    {
        public Guid? IdRol { get; set; }
        public bool Estado { get; set; }        
        public string? NombreRol { get; set; }
        public bool AccesoTodos { get; set; }
        public bool RolRestringido { get; set; }
        public Guid? IdPaginaInicioRol { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime Fechacreacion { get; set; }
        public DateTime? Fechamodficacion { get; set; }
        public List<ModuloDTOCompleto>? listaModulos { get; set; }

    }//class

}
