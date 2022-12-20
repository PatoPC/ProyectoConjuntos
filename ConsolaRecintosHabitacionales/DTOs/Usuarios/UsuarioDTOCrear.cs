using DTOs.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Usuarios
{
    public class UsuarioDTOCrear
    {
        public Guid IdRol { get; set; }

        public Guid IdPersona { get; set; }

        public Guid IdConjuntoDefault { get; set; }

        public bool? Estado { get; set; }
        public string? IdentificacionPersona { get; set; } = null!;
        public string? NombresCompletos { get; set; } = null!;
        public string? CorreoElectronico { get; set; }

        public bool ContrasenaInicial { get; set; }

        public string? Contrasena { get; set; } = null!;

        public string? UsuarioCreacion { get; set; } = null!;     

        public List<UsuarioConjuntoDTO>? UsuarioConjuntos { get; set; } = new List<UsuarioConjuntoDTO>();
    }
}
