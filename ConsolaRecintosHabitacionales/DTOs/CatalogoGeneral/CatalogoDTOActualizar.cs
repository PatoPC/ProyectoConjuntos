using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTOs.CatalogoGeneral
{
    public class CatalogoDTOActualizar
    {
        public Guid IdCatalogopadre { get; set; }
        public Guid? IdConjunto { get; set; }
        public string? Nombrecatalogo { get; set; }
        public string? Codigocatalogo { get; set; }
        public string? Descripcion { get; set; }
        public bool Editable { get; set; }
        public int? NivelCatalogo { get; set; }
        public bool Estado { get; set; }
        public string? DatoIcono { get; set; }
        public string? Datoadicional { get; set; }      
        public string? Usuariomodificacion { get; set; }

    }
}
