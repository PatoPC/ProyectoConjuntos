using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.Comprobantes;

namespace DTOs.Adeudo
{
    public class AdeudoDTOPagar
    {
        public Guid IdFormapago { get; set; }
        public decimal SaldoPendiente { get; set; }
        public bool EstadoAdeudos { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
