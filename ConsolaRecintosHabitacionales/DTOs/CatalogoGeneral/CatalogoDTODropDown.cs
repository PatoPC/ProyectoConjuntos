using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.CatalogoGeneral
{
    public class CatalogoDTODropDown
    {
        public Guid IdCatalogo { get; set; }
        public string Nombrecatalogo { get; set; }
        public string CodigoCatalogo { get; set; }
        public string Datoadicional { get; set; }
        public Guid? IdCatalogopadre { get; set; }
    }
}
