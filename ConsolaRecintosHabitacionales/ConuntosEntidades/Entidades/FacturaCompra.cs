using System;
using System.Collections.Generic;

namespace ConuntosEntidades.Entidades
{
    public partial class FacturaCompra
    {
        public Guid IdCompras { get; set; }
        public Guid? IdProvee { get; set; }
        public string? SerieCompras { get; set; }
        public int? NExternoCompras { get; set; }
        public int? TipoFacCompras { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime? FechaVenciCompra { get; set; }
        public int CodigoProveedCompra { get; set; }
        public string DetalleCompra { get; set; } = null!;
        public bool? AnuladoCompra { get; set; }
        public decimal SubtotalCompra { get; set; }
        public decimal? SubtotIvaCompra { get; set; }
        public decimal? NoGravadoCompras { get; set; }
        public decimal? IvaCompras { get; set; }
        public decimal? PorcIvaCompras { get; set; }
        public decimal? SaldoAntCompras { get; set; }
        public string? ConcepRfCompras { get; set; }
        public string? ConcepRf2Compras { get; set; }
        public decimal? PorcRfCompras { get; set; }
        public decimal? PorcRf2Compras { get; set; }
        public decimal? ValretRfCompras { get; set; }
        public decimal? ValretRf2Compras { get; set; }
        public string? ConcepIvaCompras { get; set; }
        public string? ConcepIva2Compras { get; set; }
        public decimal? PorcrIvaCompras { get; set; }
        public decimal? PorcIva2Compras { get; set; }
        public decimal? ValretIvaCompras { get; set; }
        public decimal? ValretIva2Compras { get; set; }
        public int? NroRetenCompras { get; set; }
        public string? AutSriCompras { get; set; }
        public int? SustentoCompras { get; set; }
        public DateTime? FechaCadCompras { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }

        public virtual Proveedore? IdProveeNavigation { get; set; }
    }
}
