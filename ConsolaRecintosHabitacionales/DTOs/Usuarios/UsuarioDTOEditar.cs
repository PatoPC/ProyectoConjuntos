using DTOs.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Usuarios
{
    public class UsuarioDTOEditar
    {
        public Guid? IdRol { get; set; }

        public Guid? IdPersona { get; set; }

        public Guid? IdConjuntoDefault { get; set; }

        public bool Estado { get; set; }

        public string? CorreoElectronico { get; set; }        

        public string? UsuarioModificacion { get; set; } = null!;
        public List<UsuarioConjuntoDTO>? UsuarioConjuntos { get; set; } = new List<UsuarioConjuntoDTO>();
    }
}
