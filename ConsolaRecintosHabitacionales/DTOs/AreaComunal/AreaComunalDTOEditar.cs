using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.AreaComunal
{
    public class AreaComunalDTOEditar
    {
        public Guid IdAreaComunalEditar { get; set; }
        public Guid IdConjuntoAreaComunal { get; set; }
        public string NombreAreaEditar { get; set; } = null!;
        public TimeSpan? HoraInicioEditar { get; set; }
        public TimeSpan? HoraFinEditar { get; set; }
        public bool EstadoEditar { get; set; }
        public string? ObservacionEditar { get; set; }        
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; } = null!;
    }
}
