using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Parametro
{
   public class ParametroCrearDTO

    {
        public string NombreParametro { get; set; } = null!;
        public Guid IdConjunto { get; set; }
        public Guid CtaCont1 { get; set; }
        public Guid? CtaCont2 { get; set; }
        public Guid? CtaCont3 { get; set; }
        public Guid? CtaCont4 { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
    

    }
}
