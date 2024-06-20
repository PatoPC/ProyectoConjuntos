using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ConfiguracionCuenta
{
    public class ConfiguraCuentasDTOCompleto
    {
        public Guid IdConfiguracionCuenta { get; set; }

        public Guid IdConjunto { get; set; }
        public string NombreConjunto { get; set; }

        public string Parametrizacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;
    }
}
