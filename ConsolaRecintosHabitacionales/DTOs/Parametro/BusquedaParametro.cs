using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Parametro
{
   public class BusquedaParametro
    {
        public Guid IdParametro { get; set; }
        public Guid IdConjunto { get; set; }
        public string? NombreParametro { get; set; } = null!;
        public Guid CtaCont1 { get; set; }
        public Guid? CtaCont2 { get; set; }
        public Guid? CtaCont3 { get; set; }
        public Guid? CtaCont4 { get; set; }
        public bool Estado { get; set; }
        public Guid? IdModulo { get; set; } //Se busca un catalogo que se trata como un modulo
    }
}
