using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class Catalogo
    {
        public Catalogo()
        {
            InverseIdCatalogoPadreNavigation = new HashSet<Catalogo>();
            TipoPersonas = new HashSet<TipoPersona>();
        }

        public Guid IdCatalogos { get; set; }
        public Guid? IdCatalogoPadre { get; set; }
        public string? NombreCatalogo { get; set; }
        public string? CodigoCatalogo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }

        public virtual Catalogo? IdCatalogoPadreNavigation { get; set; }
        public virtual ICollection<Catalogo> InverseIdCatalogoPadreNavigation { get; set; }
        public virtual ICollection<TipoPersona> TipoPersonas { get; set; }
    }
}
