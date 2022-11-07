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
        public decimal AliqDepto { get; set; }
        public string CoigoDepto { get; set; } = null!;
        public decimal MetrosDepto { get; set; }
        public decimal? SaldoInicialAnual { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
    }
}
