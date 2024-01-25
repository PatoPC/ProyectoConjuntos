using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ReservaArea
{
    public class ReservaAreaDTOCrear
    {
        public Guid IdAreaComunal { get; set; }
        public Guid IdPersona { get; set; }
        public string? Nombre { get; set; }
        public byte[]? Apellido { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;

    }
}
