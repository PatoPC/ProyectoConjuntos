using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTOs.CatalogoGeneral
{
    public class CatalogoDTOCompleto
    {
        public Guid IdCatalogo { get; set; }
        public Guid IdCatalogopadre { get; set; }
        public Guid IdConjunto { get; set; }
        public string? Nombrecatalogo { get; set; }
        public string? Codigocatalogo { get; set; }
        public string? Descripcion { get; set; }
        public bool Editable { get; set; }
        public bool Estado { get; set; }
        public string? Datoadicional { get; set; }
        public string? CodigoCatalogoPadre { get; set; }
        public string? NombreCatalogoPadre { get; set; }
        public string? DatoIcono { get; set; }
        public int? NivelCatalogo { get; set; }
        public DateTime Fechacreacion { get; set; }
        public DateTime? Fechamodificacion { get; set; }
        public bool TieneVigencia { get; set; }
        public string? Usuariocreacion { get; set; }
        public string? Usuariomodificacion { get; set; }
        public CatalogoDTOResultadoBusqueda? IdCatalogopadreNavigation { get; set; }
    }
}
