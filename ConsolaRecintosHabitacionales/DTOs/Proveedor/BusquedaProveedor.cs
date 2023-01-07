using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Proveedor
{
    public class BusquedaProveedor
    {
        public Guid? IdProveedor { get; set; }
        public Guid? IdConjunto { get; set; }
        public string? NombreProveedor { get; set; } = null!;
        public string? RucProveedor { get; set; } = null!;
        public string? EMailProveedor { get; set; } = null!;
    }
}
