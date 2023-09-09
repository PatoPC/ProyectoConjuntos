using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.AreaComunal
{
    public class AreaComunalDTOCrear
    {
        public AreaComunalDTOCrear(Guid idConjunto)
        {
            IdConjunto = idConjunto;
        }
        public AreaComunalDTOCrear()
        {

        }

        public Guid IdConjunto { get; set; }
        public string NombreArea { get; set; } = null!;
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public string? Observacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
    }
}
