using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.MaestroContable
{
    public class MaestroContableDTOEditar
    {
        public Guid IdConjunto { get; set; }
        public string CuentaCon { get; set; } = null!;
        public string NombreCuenta { get; set; } = null!;
        public bool Grupo { get; set; }
        public Guid IdConMstPadre { get; set; }
        public string UsuarioModificacion { get; set; } = null!;
    }
}
