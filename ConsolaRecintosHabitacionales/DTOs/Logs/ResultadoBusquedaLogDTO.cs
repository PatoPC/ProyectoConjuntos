using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Logs
{
    public class ResultadoBusquedaLogDTO
    {
        public Guid IdLogExcepciones { get; set; }
        public Guid? IdUsuario { get; set; }
        public string Metodo { get; set; }
        public string Entidad { get; set; }
        public string Error { get; set; }
        public DateTime FechaError { get; set; }
        
    }
}
