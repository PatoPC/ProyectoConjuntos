using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Adeudo
{
    public class GenerarAdeudo

    {
        public Guid? IdDepartamento { get; set; }

        public string IdConjunto { get; set; } = null!;
        public int  anio { get; set; }
        public int mes { get; set; }
        
         public int tipoGeneracion { get; set; }

    }
}
