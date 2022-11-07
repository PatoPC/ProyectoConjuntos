using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Departamento
{
    public class DepartamentoDTOCompleto
    {
        public Guid IdDepto { get; set; }
        public Guid IdTorres { get; set; }
        public decimal AliqDepto { get; set; }
        public string CoigoDepto { get; set; } = null!;
        public decimal MetrosDepto { get; set; }
        public decimal SaldoInicialAnual { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
