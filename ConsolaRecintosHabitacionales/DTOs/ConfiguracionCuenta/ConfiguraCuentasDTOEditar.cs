using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ConfiguracionCuenta
{
    public class ConfiguraCuentasDTOEditar
    {
        public Guid IdConjunto { get; set; }

        public string Parametrizacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;
    }
}
