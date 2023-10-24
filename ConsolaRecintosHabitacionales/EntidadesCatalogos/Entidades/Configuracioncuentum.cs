using System;
using System.Collections.Generic;

namespace EntidadesCatalogos.Entidades;

public partial class Configuracioncuentum
{
    public Guid IdConfiguracionCuenta { get; set; }

    public Guid IdConjunto { get; set; }

    public string Parametrizacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public string UsuarioModificacion { get; set; } = null!;
}
