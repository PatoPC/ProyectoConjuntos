using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Contabilidad
{
    public class SecuencialCabeceraContDTO
    {
        public Guid IdSecuencialCabeceraCont { get; set; }
        public Guid? IdEncCont { get; set; }
        public int Secuencial { get; set; }
    }
}
