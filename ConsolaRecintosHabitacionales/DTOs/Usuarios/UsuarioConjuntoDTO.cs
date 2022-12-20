using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Usuarios
{
    public class UsuarioConjuntoDTO
    {
        public Guid IdUsuairoConjunto { get; set; }

        public Guid IdUsuario { get; set; }

        public Guid IdConjunto { get; set; }
        public string? NombreConjunto { get; set; }
    }
}
