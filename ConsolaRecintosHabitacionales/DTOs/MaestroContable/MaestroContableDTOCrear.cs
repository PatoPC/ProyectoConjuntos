using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.MaestroContable
{
    public class MaestroContableDTOCrear
    {
        public Guid IdConjunto { get; set; }
        public string CuentaCon { get; set; } = null!;
        public string NombreCuenta { get; set; } = null!;
        public Guid? IdConMstPadre { get; set; } 
        public bool Grupo { get; set; }  
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }      
        public DateTime? FechaModificacion { get; set; }
    }
}
