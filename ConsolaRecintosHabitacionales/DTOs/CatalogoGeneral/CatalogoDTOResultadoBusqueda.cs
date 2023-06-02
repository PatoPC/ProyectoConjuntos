using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.CatalogoGeneral
{
    public class CatalogoDTOResultadoBusqueda
    {
        public Guid IdCatalogo { get; set; }
        public Guid? IdCatalogopadre { get; set; }
        public Guid? IdConjunto { get; set; }
        public string? NombreCatalogo { get; set; }
        public string? NombreConjunto { get; set; }
        public string? Codigocatalogo { get; set; }
        public string? CodigoCatalogoPadre { get; set; }
        public string? NombreCatalogoPadre { get; set; }
        public string? Datoadicional { get; set; }
        public bool TieneVigencia { get; set; }
        public string? DatoIcono { get; set; }
        public bool Estado { get; set; }

        public string? Ctacont1 { get; set; }

        public string? Ctacont2 { get; set; }

        public string? Ctacont3 { get; set; }

        public string? Ctacont4 { get; set; }

        public List<CatalogoDTOResultadoBusqueda>? InverseIdCatalogopadreNavigation { get; set; }
        //public CatalogoCompleteDTO IdCatalogopadreNavigation { get; set; }
    }
}
