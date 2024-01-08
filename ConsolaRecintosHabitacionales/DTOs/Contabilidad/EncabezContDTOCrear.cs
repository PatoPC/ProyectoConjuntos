using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Contabilidad
{
    public class EncabezContDTOCrear
    {      
        public int TipoDocNEncCont { get; set; }
        public DateTime FechaEncCont { get; set; }
        public string ConceptoEncCont { get; set; } = null!;       
        public string? CtacontEncCont { get; set; }
        public string UsuarioCreacionEncCont { get; set; } = null!;
    }
}
