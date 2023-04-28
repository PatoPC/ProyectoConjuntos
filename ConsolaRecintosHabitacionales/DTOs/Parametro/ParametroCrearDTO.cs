using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Parametro
{
   public class ParametroCrearDTO

    {
        public Guid IdCatalogo { get; set; }

        public Guid? IdCatalogopadre { get; set; }

        public Guid? IdConjunto { get; set; }

        public int? NivelCatalogo { get; set; }

        public string NombreCatalogo { get; set; } = null!;

        public string CodigoCatalogo { get; set; } = null!;

        public string? Descripcion { get; set; }

        public bool Editable { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;

        public string? Ctacont1 { get; set; }

        public string? Ctacont2 { get; set; }

        public string? Ctacont3 { get; set; }

        public string? Ctacont4 { get; set; }

 //       public  Catalogo? IdCatalogopadreNavigation { get; set; }

        public  List<ParametroCompletoDTO> InverseIdCatalogopadreNavigation { get; } = new List<ParametroCompletoDTO>();
    
       
    }
}
