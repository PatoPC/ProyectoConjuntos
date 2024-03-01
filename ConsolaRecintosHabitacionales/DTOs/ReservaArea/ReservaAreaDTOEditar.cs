using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ReservaArea
{
    public class ReservaAreaDTOEditar
    {
        public Guid IdReservaArea { get; set; }
        public Guid IdAreaComunal { get; set; }
        public Guid IdPersona { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

    }
}
