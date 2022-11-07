using System;
using System.Collections.Generic;

namespace ConuntosEntidades.Entidades
{
    public partial class ReciboDetalle
    {
        public Guid IdReciboDet { get; set; }
        public Guid IdReciboCab { get; set; }
        public int? NroReciboReciboCab { get; set; }
        public DateTime? FechaReciboDet { get; set; }
        public int? TipoDocNReciboDet { get; set; }
        public int? TipoPagoReciboDet { get; set; }
        public decimal? ValorReciboDet { get; set; }
        public int? CodCliReciboDet { get; set; }
        public string? CodDeptoReciboDet { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }

        public virtual ReciboCabecera? ReciboCabecera { get; set; }
    }
}
