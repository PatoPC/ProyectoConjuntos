using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.CatalogoGeneral
{
    public class CatalogoDTOPaginaDropDown
    {
        public Guid IdCatalogo { get; set; }
        public string? Nombrecatalogo { get; set; }        
        public string? Datoadicional { get; set; }
        public Guid IdCatalogopadre { get; set; }

        public virtual string NombrePaginaDefault { 
            get
            {
                return Nombrecatalogo + " (" + Datoadicional + ")";
            }
            
        }
    }
}
