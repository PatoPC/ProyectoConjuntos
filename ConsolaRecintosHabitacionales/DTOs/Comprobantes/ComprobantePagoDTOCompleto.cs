using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Comprobantes
{
    public class ComprobantePagoDTOCompleto
    {
        public Guid IdComprobantePago { get; set; }
        public Guid IdTipoPago { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal SaldoPendiente { get; set; }
        public decimal ValorPago { get; set; }
        public string? UrlConsumaTablaDeuda { get; set; } = null!;
        public string? Concepto { get; set; } = null!;
        public string? TipoPago { get; set; } = null!;
        public string? Observacion { get; set; }
        public bool EstadoImpreso { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Conjunto { get; set; } = null!;
        public string? Torre { get; set; } = null!;
        public string? Departamento { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public List<DetalleComprobantePagoDTOCompleto> DetalleComprobantePagos { get; set; }
        public List<SecuencialComprobantePagoDTO> SecuencialComprobantePagos { get; set; }
    }
}
