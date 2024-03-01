using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ReservaArea
{
    public class BusquedaReservaAreaDTO
    {
        public Guid IdConjunto { get; set; }
        public Guid IdReservaArea { get; set; }
        public Guid IdAreaComunal { get; set; }
        public Guid IdPersona { get; set; }
    }
}
