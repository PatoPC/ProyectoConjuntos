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
        public Guid IdConjunto { get; set; }
        public bool? Estado { get; set; }
        public string? NombreConjunto { get; set; }
        public string? numeroIdentificacion { get; set; }
        public string? Perfil { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public List<UsuarioConjuntoDTO>? UsuarioConjuntos { get; set; } = new List<UsuarioConjuntoDTO>();
    }
}
