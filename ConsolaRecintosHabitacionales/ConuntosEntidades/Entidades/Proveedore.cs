using System;
using System.Collections.Generic;

namespace ConuntosEntidades.Entidades
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            FacturaCompras = new HashSet<FacturaCompra>();
        }

        public Guid IdProvee { get; set; }
        public string NombreProvee { get; set; } = null!;
        public string RucProvee { get; set; } = null!;
        public string? ContactoProvee { get; set; }
        public string CiudadProvee { get; set; } = null!;
        public string DirecProvee { get; set; } = null!;
        public string? TelefonosProvee { get; set; }
        public string? EMailProvee { get; set; }
        public decimal? SaldoAntProvee { get; set; }
        public decimal? SaldoPendProvee { get; set; }
        public bool? StatusProvee { get; set; }

        public virtual ICollection<FacturaCompra> FacturaCompras { get; set; }
    }
}
