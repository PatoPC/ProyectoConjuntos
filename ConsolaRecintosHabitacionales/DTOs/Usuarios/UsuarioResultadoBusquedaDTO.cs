using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Usuarios
{
    public class UsuarioResultadoBusquedaDTO
    {
        public Guid IdUsuario { get; set; }
        public Guid IdPersona { get; set; }
        public bool? Estado { get; set; }
        public string Empresa { get; set; }
        public string numeroIdentificacion { get; set; }
        public string Perfil { get; set; }
        public string CorreoElectronico { get; set; }
        public string Nombre { get; set; }
        public string EstadoLaboral { get; set; }
        public string Apellido { get; set; }
    }
}
