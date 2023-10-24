using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.MaestroContable
{
    public class MaestroContableDTOCompleto
    {
        public Guid IdConMst { get; set; }
        public Guid IdConjunto { get; set; }
        public string CuentaCon { get; set; } = null!;
        public string NombreCuenta { get; set; } = null!;
        public bool Grupo { get; set; }
        public Guid IdConMstPadre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }
        public List<MaestroContableDTOCompleto> InverseIdConMstPadreNavigation { get; set; }
    }
}
