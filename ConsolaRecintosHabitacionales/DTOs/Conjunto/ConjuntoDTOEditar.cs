using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Conjunto
{
    public class ConjuntoDTOEditar
    {
       
        public string NombreConjunto { get; set; } = null!;
        public string RucConjunto { get; set; } = null!;
        public string DireccionConjunto { get; set; } = null!;
        public string TelefonoConjunto { get; set; } = null!;
        public string MailConjunto { get; set; } = null!;
       
        public string? UsuarioModificacion { get; set; }

    }
}
