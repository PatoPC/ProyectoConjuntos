using DTOs.AreasDepartamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Departamento
{
    public class DepartamentoDTOCrear
    {
        public DepartamentoDTOCrear() { }
        public DepartamentoDTOCrear(Guid idConjuntoCrearDepartamento)
        {
            IdConjuntoCrearDepartamento = idConjuntoCrearDepartamento;
        }

        public Guid IdTorresCrearDepartamento { get; set; }        

        public Guid IdConjuntoCrearDepartamento { get; set; }
        public Guid? IdConMst { get; set; }
        public decimal? AliqDepartamento { get; set; }
        public string CodigoDepartamento { get; set; } = null!;
        public decimal? MetrosDepartamento { get; set; }
        public decimal? SaldoInicialAnual { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public List<AreasDepartamentoDTO>? AreasDepartamentos { get; set; }
    }
}
