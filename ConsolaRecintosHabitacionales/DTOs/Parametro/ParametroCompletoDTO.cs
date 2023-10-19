using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Parametro
{
   public class ParametroCompletoDTO
    {
        public Guid IdParametro { get; set; }
        public Guid IdConjunto { get; set; }
        public string NombreParametro { get; set; } = null!;
        public Guid? IdModulo { get; set; }
        public string Cuenta1 { get; set; } = null!;
        public Guid CtaCont1 { get; set; }
        public string Cuenta2 { get; set; } = null!;
        public Guid? CtaCont2 { get; set; }
        public string Cuenta3 { get; set; } = null!;
        public Guid? CtaCont3 { get; set; }
        public string Cuenta4 { get; set; } = null!;
        public Guid? CtaCont4 { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
    }
}
