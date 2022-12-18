using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Usuarios
{
    public class UsuarioCambioContrasena
    {
        public Guid IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NuevaContrasena { get; set; }
        public bool Aspirante { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasena { get; set; }
        public string ContraseniaConfirma { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
