using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Comunicado
{
    public class BusquedaComunicadoDTO
    {
        public string? Titulo { get; set; } = null!;
        public Guid IdConjunto { get; set; } 
        public string? Descripcion { get; set; } = null!;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? Estado { get; set; }
    }
}
