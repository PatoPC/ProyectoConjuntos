using DTOs.AreasDepartamento;
using DTOs.ConfiguracionCuenta;
using DTOs.MaestroContable;
using DTOs.Persona;
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
        public List<AreasDepartamentoDTO>? AreasDepartamentos { get; set; }
        public MaestroContableDTOCrear? IdConMstNavigation { get; set; }
        public virtual List<TipoPersonaDTO>? TipoPersonas { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
