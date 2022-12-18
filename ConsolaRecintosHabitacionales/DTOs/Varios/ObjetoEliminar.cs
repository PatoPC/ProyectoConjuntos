using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Varios
{
    public class ObjetoEliminar
    {
        public Guid IdObjetoEliminado { get; set; }
        public string DatosEliminados { get; set; }
        public string TipoDatoEliminado { get; set; }
        public Guid IdPersonaEliminar { get; set; }
        public DateTime FechaEliminacion { get; set; }
        public string UsuarioElimina { get; set; }
        public Guid IdUsuarioElimina { get; set; }
        public string RutaArchivo { get; set; }
        public string NumeroIdentificacion { get; set; }
    }
}
