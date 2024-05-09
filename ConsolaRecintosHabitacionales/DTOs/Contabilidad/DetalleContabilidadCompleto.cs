using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Contabilidad
{
    public class DetalleContabilidadCompleto
    {
        public Guid IdDetCont { get; set; }
        public Guid? IdEncCont { get; set; }
        public DateTime FechaDetCont { get; set; }
        public string? NroDepartmentoCont { get; set; }
        public string DetalleDetCont { get; set; } = null!;
        public Guid IdCuentaContable { get; set; }
        public decimal? DebitoDetCont { get; set; }
        public decimal? CreditoDetCont { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string CuentaContable { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }

    }
}
