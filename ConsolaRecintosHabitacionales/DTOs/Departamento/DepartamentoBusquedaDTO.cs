using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Departamento
{
    public class DepartamentoBusquedaDTO
    {
        public DepartamentoBusquedaDTO() { }
        public DepartamentoBusquedaDTO(Guid idTorres)
        {
            IdTorres = idTorres;
        }
        public DepartamentoBusquedaDTO(string coigoDepto)
        {
            CoigoDepto = coigoDepto;
        }

        public Guid IdDepto { get; set; }
        public Guid IdTorres { get; set; }
        
        public string CoigoDepto { get; set; } = null!;

        
    }
}
