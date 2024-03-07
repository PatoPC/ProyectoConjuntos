using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Contabilidad
{
    public class EncabezContDTOCrear
    {
        public Guid? IdConjunto { get; set; }
        public Guid TipoDocNEncCont { get; set; }
        public int NCompEncCont { get; set; }
        public DateTime FechaEncCont { get; set; }
        public int? ChequeEncCont { get; set; }
        public string ConceptoEncCont { get; set; } = null!;
        public bool? AnuladoEncCont { get; set; }
        public DateTime? FecAnulaEncCont { get; set; }
        public DateTime? FecVenciEncCont { get; set; }
        public string UsuarioCreacionEncCont { get; set; } = null!;
        public List<DetalleContabilidadCrear> DetalleContabilidads { get; set; }
    }
}
