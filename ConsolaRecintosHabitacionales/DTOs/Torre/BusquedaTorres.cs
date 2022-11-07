using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Torre
{
    public class BusquedaTorres
    {
        public Guid IdTorres { get; set; }
        public Guid IdConjunto { get; set; }
        public string? NombreTorres { get; set; } = null!;
    }
}
