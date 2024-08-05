using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Adeudo
{
    public class AdeudoDTOEditar
    {
        public Guid IdFormapago { get; set; }
        public decimal MontoAdeudos { get; set; }
        public decimal SaldoPendiente { get; set; }
        public decimal valorPagar { get; set; }
        public bool EstadoAdeudos { get; set; }
        public string Observacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaPago { get; set; }
        public virtual List<PagoAdeudoDTOCompleto> PagoAdeudos { get; set; }
    }
}
