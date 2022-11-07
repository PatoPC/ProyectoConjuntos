using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Torre
{
    public class TorreDTOCrear
    {      
        public TorreDTOCrear()
        {          
        }
        public TorreDTOCrear(Guid? idConjunto)
        {
            IdConjunto = idConjunto;
        }

        public Guid? IdConjunto { get; set; }
        public string NombreTorres { get; set; } = null!;      
        public string UsuarioCreacion { get; set; } = null!;
    }
}
