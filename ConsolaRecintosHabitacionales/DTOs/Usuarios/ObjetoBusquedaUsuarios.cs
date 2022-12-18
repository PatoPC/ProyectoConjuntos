using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Usuarios
{
    public class ObjetoBusquedaUsuarios
    {
        public Guid? IdRol { get; set; }
        public Guid? IdConjunto { get; set; }
        public string? numeroIdentificacion { get; set; }
        public string? nombres { get; set; }
        public string? apellidos { get; set; }
    }
}
