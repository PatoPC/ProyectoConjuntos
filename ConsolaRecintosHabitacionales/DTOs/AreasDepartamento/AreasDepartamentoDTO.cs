using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.AreasDepartamento
{
    public class AreasDepartamentoDTO
    {
        public AreasDepartamentoDTO() { }
        public AreasDepartamentoDTO(Guid? idTipoArea, decimal? metrosCuadrados)
        {
            IdTipoArea = idTipoArea;
            MetrosCuadrados = metrosCuadrados;
        }
        public Guid? IdAreasDepartamentos { get; set; }
        public Guid? IdDepartamento { get; set; }
        public Guid? IdTipoArea { get; set; }
        public decimal? MetrosCuadrados { get; set; }
        public string? NombreTipoArea { get; set; }
    }
}
