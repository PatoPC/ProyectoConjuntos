using DTOs.Torre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Conjunto
{
    public class ConjuntoDTOCompleto
    {
        public Guid IdConjunto { get; set; }
        public string? NombreConjunto { get; set; } 
        public string? RucConjunto { get; set; } 
        public string? DireccionConjunto { get; set; } 
        public string? TelefonoConjunto { get; set; } 
        public string? MailConjunto { get; set; } 
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioCreacion { get; set; } 
        public string? UsuarioModificacion { get; set; }


        public List<TorreDTOCompleto>? Torres { get; set; }

    }
}
