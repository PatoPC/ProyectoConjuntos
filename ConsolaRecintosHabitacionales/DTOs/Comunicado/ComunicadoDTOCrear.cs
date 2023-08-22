using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Comunicado
{
    public class ComunicadoDTOCrear
    {      
        public Guid IdConjunto { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaCreacion { get; set; }       
        public string UsuarioCreacion { get; set; } = null!;
    }
}
