using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Contabilidad
{
    public class DetalleContabilidadCrear
    {
        public Guid? IdEncCont { get; set; }
        public DateTime FechaDetCont { get; set; }
        public string CtacontDetCont { get; set; } = null!;
        public string NroIntDetCont { get; set; } = null!;
        public string DetalleDetCont { get; set; } = null!;
        public decimal? DebitoDetCont { get; set; }
        public decimal? CreditoDetCont { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }

    }
}
