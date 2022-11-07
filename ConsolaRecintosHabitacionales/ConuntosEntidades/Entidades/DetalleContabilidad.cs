using System;
using System.Collections.Generic;

namespace ConuntosEntidades.Entidades
{
    public partial class DetalleContabilidad
    {
        public Guid IdDetCont { get; set; }
        public Guid? IdEncCont { get; set; }
        public int NCompDetCont { get; set; }
        public int TipoDocNDetCont { get; set; }
        public DateTime FechaDetCont { get; set; }
        public string CtacontDetCont { get; set; } = null!;
        public int? NroIntDetCont { get; set; }
        public string? NnroExternoDetCont { get; set; }
        public string DetalleDetCont { get; set; } = null!;
        public decimal? DebitoDetCont { get; set; }
        public decimal? CreditoDetCont { get; set; }
        public int? ChequeDetCont { get; set; }
        public DateTime? FechaVenciDetCont { get; set; }
        public int? TipoCodDetCont { get; set; }
        public int? CodigoProvDetCont { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }

        public virtual EncabezadoContabilidad? IdEncContNavigation { get; set; }
    }
}
