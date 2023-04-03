using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.MaestroContable
{
    public class MaestroContableBusqueda
    {
        public Guid? IdConjunto { get; set; } = null!;
        public string? CuentaCon { get; set; } = null!;
        public string? NombreCuenta { get; set; } = null!;
        public bool? Grupo { get; set; } = null!;
    }
}
