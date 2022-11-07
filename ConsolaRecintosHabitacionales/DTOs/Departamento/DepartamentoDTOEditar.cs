using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Departamento
{
    public class DepartamentoDTOEditar
    {
        public DepartamentoDTOEditar() { }
        public Guid IdConjuntoEditarDepartamento { get; set; }

        public DepartamentoDTOEditar(Guid idConjuntoEditarDepartamento)
        {
            IdConjuntoEditarDepartamento = idConjuntoEditarDepartamento;
        }

        public Guid IdDeptoEditar { get; set; }
        public Guid IdTorresEditarDepartamento { get; set; }
        public decimal AliqDeptoEditar { get; set; }
        public string CoigoDeptoEditar { get; set; } = null!;
        public decimal MetrosDeptoEditar { get; set; }
        public decimal SaldoInicialAnualEditar { get; set; }
        public string UsuarioModificacion { get; set; }

    }
}
