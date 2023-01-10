using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            FacturaCompras = new HashSet<FacturaCompra>();
        }

        public Guid IdProveedor { get; set; }
        public Guid IdConjunto { get; set; }
        public string NombreProveedor { get; set; } = null!;
        public string RucProveedor { get; set; } = null!;
        public string? ContactoProveedor { get; set; }
        public string DirecProveedor { get; set; } = null!;
        public string? TelefonosProveedor { get; set; }
        public string? EMailProveedor { get; set; }
        public decimal? SaldoAntProveedor { get; set; }
        public decimal? SaldoPendProveedor { get; set; }
        public bool StatusProveedor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }
        public Guid? IdCiudadProveedor { get; set; }

        public virtual Conjunto IdConjuntoNavigation { get; set; } = null!;
        public virtual ICollection<FacturaCompra> FacturaCompras { get; set; }
    }
}
