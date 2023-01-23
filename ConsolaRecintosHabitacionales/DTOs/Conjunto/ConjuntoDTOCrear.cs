using DTOs.Torre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Conjunto
{
    public class ConjuntoDTOCrear
    {
        public string? NombreConjunto { get; set; } 
        public string? RucConjunto { get; set; } 
        public string? DireccionConjunto { get; set; } 
        public string? TelefonoConjunto { get; set; } 
        public string? MailConjunto { get; set; } 
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public List<TorreDTOCrear>? Torres { get; set; }
      
    }
}
