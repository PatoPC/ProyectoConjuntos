using System;
using System.Collections.Generic;

namespace EntidadesCatalogos.Entidades;

public partial class Catalogo
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

    public bool TieneVigencia { get; set; }

    public string? DatoAdicional { get; set; }

    public string? DatoIcono { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public string UsuarioModificacion { get; set; } = null!;

    public virtual Catalogo? IdCatalogopadreNavigation { get; set; }

    public virtual ICollection<Catalogo> InverseIdCatalogopadreNavigation { get; } = new List<Catalogo>();
}
