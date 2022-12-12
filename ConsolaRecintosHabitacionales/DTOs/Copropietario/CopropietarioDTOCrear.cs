using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Copropietario
{
    public class CopropietarioDTOCrear
    {
        public Guid? IdArrendatario { get; set; }
        public string NombreCondomino { get; set; } = null!;
        public string RucCondomino { get; set; } = null!;
        public string? TelefonoCondomino { get; set; }
        public string? EmailCondomino { get; set; }
        public string? CodigoDeptoCondomino { get; set; }
        public string? ObservacionCondomino { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }
    }
}
