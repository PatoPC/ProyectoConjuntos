using DTOs.AreasDepartamento;
using DTOs.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Departamento
{
    public class DepartamentoDTOCompleto
    {
        public Guid IdDepartamento { get; set; }
        public Guid IdTorres { get; set; }
        public Guid? IdConMst { get; set; }
        public decimal AliqDepartamento { get; set; }
        public string CodigoDepartamento { get; set; } = null!;
        public decimal MetrosDepartamento { get; set; }
        public decimal? SaldoInicialAnual { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public List<TipoPersonaDTO>? TipoPersonas { get; set; }
        public List<AreasDepartamentoDTO>? AreasDepartamentos { get; set; }
    }
}
